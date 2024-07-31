/*
 * Licensed under the MIT License.
 * For details on the licensing terms, see the LICENSE file.
 * SPDX-License-Identifier: MIT
 *
 * Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)
 */

#include <stdio.h>
#include "signal.h"
#include "unistd.h"
#include <open62541/plugin/log_stdout.h>
#include <open62541/server.h>
#include <pthread.h>
#include "namespace_pfdl_parameter_generated.h"
#include "namespace_warehouse_generated.h"
#include "namespace_common_generated.h"
#include "namespace_coating_generated.h"
#include "namespace_milling_generated.h"
#include "namespace_gluing_generated.h"
#include "namespace_mounting_generated.h"
#include "types_common_generated.h"

#include "custom_behavior/service_callbacks.h"
#include "swap_it.h"

UA_Boolean running = true;
static void stopHandler(int sign) {
    UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "received ctrl-c");
    running = false;
}

static UA_ByteString loadFile(const char *const path) {
    UA_ByteString fileContents = UA_STRING_NULL;
    /* Open the file */
    FILE *fp = fopen(path, "rb");
    if(!fp) {
        //errno = 0; /* We read errno also from the tcp layer... */
        printf("failed to load the file\n");
        return fileContents;
    }
    /* Get the file length, allocate the data and read */
    fseek(fp, 0, SEEK_END);
    fileContents.length = (size_t)ftell(fp);
    fileContents.data = (UA_Byte *)UA_malloc(fileContents.length * sizeof(UA_Byte));
    if(fileContents.data) {
        fseek(fp, 0, SEEK_SET);
        size_t read = fread(fileContents.data, sizeof(UA_Byte), fileContents.length, fp);
        if(read != fileContents.length)
            UA_ByteString_clear(&fileContents);
    } else {
        fileContents.length = 0;
    }
    fclose(fp);

    return fileContents;
}

typedef struct{
    char* path_to_config;
    size_t namespace_position;
    UA_Boolean *running;
    pthread_t threadId;
    UA_MethodCallback methodcallback;
}server_info;

typedef struct{
    size_t number_server;
    server_info *server;
}server_dict;


UA_StatusCode (* load_namespace[])(UA_Server *server) = {
        namespace_pfdl_parameter_generated,
        namespace_warehouse_generated,
        namespace_coating_generated,
        namespace_milling_generated,
        namespace_gluing_generated,
        namespace_mounting_generated
};

void *start_server(void *data){
    server_info *info = (server_info*) data;
    UA_Server *server = UA_Server_new();
    /*load additional swap namespaces*/
    UA_StatusCode retval = namespace_common_generated(server);
    if(retval != UA_STATUSCODE_GOOD) {
        UA_LOG_WARNING(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the common namespace failed. Please check previous error output.");
        UA_Server_delete(server);
    }
    UA_StatusCode (* import_namespace)(UA_Server * )= load_namespace[0];
    retval = import_namespace(server);
    if(retval != UA_STATUSCODE_GOOD) {
        UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the sfb_parameter namespace failed. Please check previous error output.");
        UA_Server_delete(server);
    }
    import_namespace = load_namespace[info->namespace_position];
    retval = import_namespace(server);
    if(retval != UA_STATUSCODE_GOOD) {
        UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the namespace on pos %zu in the load_namespace function failed. Please check previous error output.", info->namespace_position);
        UA_Server_delete(server);
    }
    UA_ByteString json = loadFile(info->path_to_config);
    UA_service_server_interpreter swap_server;
    memset(&swap_server, 0, sizeof(UA_service_server_interpreter));

    retval = UA_server_swap_it(server, json, info->methodcallback, UA_FALSE, &running, UA_TRUE, &swap_server);
    /*UA_Server_run_startup(server);
    UA_Server_run_iterate(server, true);*/
    if(retval != UA_STATUSCODE_GOOD){
        UA_ByteString_clear(&json);
        pthread_exit((void *) info->threadId);
    }else{
        UA_ByteString_clear(&json);
        //UA_Server_run_startup(server);
        while(running) {
            UA_Server_run_iterate(server, true);
        }
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_SERVER,"Shutting down server %s ", swap_server.server_name);
        /*unregister the agent and clear the config information*/
        clear_swap_server(&swap_server, UA_TRUE, server);
        UA_Server_run_shutdown(server);
        UA_Server_delete(server);
        pthread_exit((void *) info->threadId);
    }
}

int main() {
    signal(SIGINT, stopHandler);
    signal(SIGTERM, stopHandler);
    size_t nbr_resources = 8;
    server_dict resources;
    resources.number_server = nbr_resources;
    resources.server = (server_info*) UA_calloc(nbr_resources, sizeof(server_info));

    char *path_to_warehouse_1_config = "../configs/warehouse_config.json5";
    resources.server[0].path_to_config = (char*) UA_calloc(strlen(path_to_warehouse_1_config+1), sizeof(char*));
    strcpy(resources.server[0].path_to_config, path_to_warehouse_1_config);
    resources.server[0].running = &running;
    resources.server[0].namespace_position = 1;
    resources.server[0].methodcallback = warehousemethodCallback;

    char *path_to_coating_1_config = "../configs/coating_config.json5";
    resources.server[1].path_to_config = (char*) UA_calloc(strlen(path_to_coating_1_config+1), sizeof(char*));
    strcpy(resources.server[1].path_to_config, path_to_coating_1_config);
    resources.server[1].running = &running;
    resources.server[1].namespace_position = 2;
    resources.server[1].methodcallback = coatingmethodCallback;

    char *path_to_milling_1_config = "../configs/milling_config.json5";
    resources.server[2].path_to_config = (char*) UA_calloc(strlen(path_to_milling_1_config+1), sizeof(char*));
    strcpy(resources.server[2].path_to_config, path_to_milling_1_config);
    resources.server[2].running = &running;
    resources.server[2].namespace_position = 3;
    resources.server[2].methodcallback = millingmethodCallback;

    char *path_to_gluing_1_config = "../configs/gluing_config.json5";
    resources.server[3].path_to_config = (char*) UA_calloc(strlen(path_to_gluing_1_config+1), sizeof(char*));
    strcpy(resources.server[3].path_to_config, path_to_gluing_1_config);
    resources.server[3].running = &running;
    resources.server[3].namespace_position = 4;
    resources.server[3].methodcallback = gluingmethodCallback;

    char *path_to_mounting_1_config = "../configs/mounting_config.json5";
    resources.server[4].path_to_config = (char*) UA_calloc(strlen(path_to_mounting_1_config+1), sizeof(char*));
    strcpy(resources.server[4].path_to_config, path_to_mounting_1_config);
    resources.server[4].running = &running;
    resources.server[4].namespace_position = 5;
    resources.server[4].methodcallback = mountingmethodCallback;

    char *path_to_warehouse_2_config = "../configs/warehouse_config_2.json5";
    resources.server[5].path_to_config = (char*) UA_calloc(strlen(path_to_warehouse_2_config+1), sizeof(char*));
    strcpy(resources.server[5].path_to_config, path_to_warehouse_2_config);
    resources.server[5].running = &running;
    resources.server[5].namespace_position = 1;
    resources.server[5].methodcallback = warehousemethodCallback;

    char *path_to_coating_2_config = "../configs/coating_config_2.json5";
    resources.server[6].path_to_config = (char*) UA_calloc(strlen(path_to_coating_2_config+1), sizeof(char*));
    strcpy(resources.server[6].path_to_config, path_to_coating_2_config);
    resources.server[6].running = &running;
    resources.server[6].namespace_position = 2;
    resources.server[6].methodcallback = coatingmethodCallback;

    char *path_to_coating_3_config = "../configs/coating_config_3.json5";
    resources.server[7].path_to_config = (char*) UA_calloc(strlen(path_to_coating_3_config+1), sizeof(char*));
    strcpy(resources.server[7].path_to_config, path_to_coating_3_config);
    resources.server[7].running = &running;
    resources.server[7].namespace_position = 2;
    resources.server[7].methodcallback = coatingmethodCallback;

    printf("start the threads\n");
    for(size_t i=0; i<resources.number_server; i++){
        pthread_create(&resources.server[i].threadId, NULL, start_server, &resources.server[i]);
    }

    while(running) {
        sleep(1);
    }
    sleep(5);
    for(size_t i=0; i<resources.number_server; i++){
        free(resources.server[i].path_to_config);
    }
    free(resources.server);
}
