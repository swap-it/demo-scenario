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
#include <errno.h>
#include <pthread.h>
//include the open62541-server-template
#include "swap_it.h"
//include the generated c code from the information models
#include "namespace_common_generated.h"
#include "namespace_pfdl_parameter_generated.h"
#include "namespace_warehouse_generated.h"
#include "types_common_generated.h"
#include "types_common_generated_handling.h"
#include "types_pfdl_parameter_generated_handling.h"
#include "types_pfdl_parameter_generated.h"
#include "warehouse_nodeids.h"
#include "coating_nodeids.h"
#include "gluing_nodeids.h"
#include "namespace_coating_generated.h"
#include "namespace_gluing_generated.h"

//read in the json config file
static UA_INLINE UA_ByteString
loadFile(const char *const path) {
    UA_ByteString fileContents = UA_STRING_NULL;
    /* Open the file */
    FILE *fp = fopen(path, "rb");
    if(!fp) {
        errno = 0; /* We read errno also from the tcp layer... */
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

UA_StatusCode gluingmethodCallback(UA_Server *server,
                                      const UA_NodeId *sessionId, void *sessionHandle,
                                      const UA_NodeId *methodId, void *methodContext,
                                      const UA_NodeId *objectId, void *objectContext,
                                      size_t inputSize, const UA_Variant *input,
                                      size_t outputSize, UA_Variant *output){

    //set output variable for the Service finished event
    UA_SWAP_Order order;
    UA_SWAP_Order_init(&order);
    order.number_light_segments = 5;
    order.order_id = 1000;
    order.segmentsSize = 2;

    UA_Light_Segment *segments = (UA_Light_Segment *) UA_calloc(2, sizeof(UA_Light_Segment));
    segments[0].diameter = 5;
    segments[0].color = UA_String_fromChars("red");
    segments[0].segment_id = UA_String_fromChars("Default");
    segments[1].diameter = 5;
    segments[1].color = UA_String_fromChars("green");
    segments[1].segment_id = UA_String_fromChars("Default");

    UA_Stand_Segment stand;
    UA_Stand_Segment_init(&stand);
    stand.stand_height = 3;
    stand.stand_shape = UA_String_fromChars("plate");
    stand.stand_id = UA_String_fromChars("Default");

    order.stand = stand;
    order.segments = segments;
    //print the specified parameter
    UA_String out = UA_STRING_NULL;
    UA_print(&order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER], &out);
    printf("Order Variable: %.*s\n", (int)out.length, out.data);
    UA_String_clear(&out);

    UA_Variant temp;
    UA_Variant_init(&temp);
    UA_Variant_setScalarCopy(&temp, &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);
    //fire the service result event
    UA_NodeId eventOutNodeId;
    UA_NodeId_init(&eventOutNodeId);
    UA_StatusCode retval = UA_Server_createEvent(server, UA_NODEID_NUMERIC(4, UA_GLUINGID_GLUING), &eventOutNodeId);
    if(retval!= UA_STATUSCODE_GOOD){
        UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Failed to create the gluing Event");
        UA_NodeId_clear(&eventOutNodeId);
        return retval;
    }
    UA_Server_writeObjectProperty_scalar(server, eventOutNodeId,
                                         UA_QUALIFIEDNAME(4, "order"), &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);

    retval = UA_Server_triggerEvent(server, eventOutNodeId, UA_NODEID_NUMERIC(0, UA_NS0ID_SERVER), NULL, UA_TRUE);
    UA_NodeId_clear(&eventOutNodeId);
    if(retval!= UA_STATUSCODE_GOOD){
        UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Failed to trigger the gluing Event");
        return retval;
    }
    else{
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Trigger the event");
    }
    //create the ServiceExecutionAsyncResultDataType variable
    UA_ServiceExecutionAsyncResultDataType res;
    UA_ServiceExecutionAsyncResultDataType_init(&res);
    res.serviceResultCode = (UA_UInt32) 1;
    res.serviceResultMessage = UA_String_fromChars("Message");
    res.expectedServiceExecutionDuration = (UA_Double) 7;
    res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;
    //set the variable as method output
    UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
    UA_String_clear(&res.serviceResultMessage);
    //free allocated memory
    free(order.segments);
    return UA_STATUSCODE_GOOD;
}

UA_StatusCode coatingmethodCallback(UA_Server *server,
                                      const UA_NodeId *sessionId, void *sessionHandle,
                                      const UA_NodeId *methodId, void *methodContext,
                                      const UA_NodeId *objectId, void *objectContext,
                                      size_t inputSize, const UA_Variant *input,
                                      size_t outputSize, UA_Variant *output){
    //set output variable for the Service finished event
    UA_SWAP_Order order;
    UA_SWAP_Order_init(&order);
    order.number_light_segments = 5;
    order.order_id = 1000;
    order.segmentsSize = 2;

    UA_Light_Segment *segments = (UA_Light_Segment *) UA_calloc(2, sizeof(UA_Light_Segment));
    segments[0].diameter = 5;
    segments[0].color = UA_String_fromChars("red");
    segments[0].segment_id = UA_String_fromChars("Default");
    segments[1].diameter = 5;
    segments[1].color = UA_String_fromChars("green");
    segments[1].segment_id = UA_String_fromChars("Default");

    UA_Stand_Segment stand;
    UA_Stand_Segment_init(&stand);
    stand.stand_height = 3;
    stand.stand_shape = UA_String_fromChars("plate");
    stand.stand_id = UA_String_fromChars("Default");

    order.stand = stand;
    order.segments = segments;
    //print the specified parameter
    UA_String out = UA_STRING_NULL;
    UA_print(&order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER], &out);
    printf("Order Variable: %.*s\n", (int)out.length, out.data);
    UA_String_clear(&out);

    UA_Variant temp;
    UA_Variant_init(&temp);
    UA_Variant_setScalarCopy(&temp, &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);

    //fire the service result event
    UA_NodeId eventOutNodeId;
    UA_NodeId_init(&eventOutNodeId);
    UA_Server_createEvent(server, UA_NODEID_NUMERIC(4, UA_COATINGID_COATING), &eventOutNodeId);
    UA_Server_writeObjectProperty_scalar(server, eventOutNodeId,
                                         UA_QUALIFIEDNAME(4, "order"), &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);
    UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Trigger the event");
    UA_Server_triggerEvent(server, eventOutNodeId, UA_NODEID_NUMERIC(0, UA_NS0ID_SERVER), NULL, UA_TRUE);
    UA_NodeId_clear(&eventOutNodeId);

    //create the ServiceExecutionAsyncResultDataType variable
    UA_ServiceExecutionAsyncResultDataType res;
    UA_ServiceExecutionAsyncResultDataType_init(&res);
    res.serviceResultCode = (UA_UInt32) 1;
    res.serviceResultMessage = UA_String_fromChars("Message");
    res.expectedServiceExecutionDuration = (UA_Double) 7;
    res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;
    //set the variable as method output
    UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
    UA_String_clear(&res.serviceResultMessage);
    //free allocated memory
    free(order.segments);
    return UA_STATUSCODE_GOOD;
}

UA_StatusCode warehousemethodCallback(UA_Server *server,
                                      const UA_NodeId *sessionId, void *sessionHandle,
                                      const UA_NodeId *methodId, void *methodContext,
                                      const UA_NodeId *objectId, void *objectContext,
                                      size_t inputSize, const UA_Variant *input,
                                      size_t outputSize, UA_Variant *output){

    //set output variable for the Service finished event
    UA_SWAP_Order order;
    UA_SWAP_Order_init(&order);
    order.number_light_segments = 5;
    order.order_id = 1000;
    order.segmentsSize = 2;

    UA_Light_Segment *segments = (UA_Light_Segment *) UA_calloc(2, sizeof(UA_Light_Segment));
    segments[0].diameter = 5;
    segments[0].color = UA_String_fromChars("red");
    segments[0].segment_id = UA_String_fromChars("Default");
    segments[1].diameter = 5;
    segments[1].color = UA_String_fromChars("green");
    segments[1].segment_id = UA_String_fromChars("Default");

    UA_Stand_Segment stand;
    UA_Stand_Segment_init(&stand);
    stand.stand_height = 3;
    stand.stand_shape = UA_String_fromChars("plate");
    stand.stand_id = UA_String_fromChars("Default");

    order.stand = stand;
    order.segments = segments;
    //print the specified parameter
    UA_String out = UA_STRING_NULL;
    UA_print(&order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER], &out);
    printf("Order Variable: %.*s\n", (int)out.length, out.data);
    UA_String_clear(&out);

    UA_Variant temp;
    UA_Variant_init(&temp);
    UA_Variant_setScalarCopy(&temp, &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);

    //fire the service result event
    UA_NodeId eventOutNodeId;
    UA_NodeId_init(&eventOutNodeId);
    UA_Server_createEvent(server, UA_NODEID_NUMERIC(4, UA_WAREHOUSEID_GETPARTSFROMWAREHOUSE), &eventOutNodeId);
    UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Trigger the event");
    UA_Server_writeObjectProperty_scalar(server, eventOutNodeId,
                                         UA_QUALIFIEDNAME(4, "order"), &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);
    UA_Server_triggerEvent(server, eventOutNodeId, UA_NODEID_NUMERIC(0, UA_NS0ID_SERVER), NULL, UA_TRUE);
    UA_NodeId_clear(&eventOutNodeId);

    //create the ServiceExecutionAsyncResultDataType variable
    UA_ServiceExecutionAsyncResultDataType res;
    UA_ServiceExecutionAsyncResultDataType_init(&res);
    res.serviceResultCode = (UA_UInt32) 1;
    res.serviceResultMessage = UA_String_fromChars("Message");
    res.expectedServiceExecutionDuration = (UA_Double) 7;
    res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;
    //set the variable as method output
    UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
    UA_String_clear(&res.serviceResultMessage);
    //free allocated memory
    free(order.segments);
    return UA_STATUSCODE_GOOD;
}

UA_StatusCode (* load_namespace[])(UA_Server *server) = {
        namespace_pfdl_parameter_generated,
        namespace_warehouse_generated,
        namespace_coating_generated,
        namespace_gluing_generated
};

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

void *start_server(void *data){
    server_info *info = (server_info*) data;
    UA_Server *server = UA_Server_new();
    /*load the common namespace*/
    UA_StatusCode retval = namespace_common_generated(server);
    if(retval != UA_STATUSCODE_GOOD) {
        UA_LOG_WARNING(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the common namespace failed. Please check previous error output.");
        UA_Server_delete(server);
    }
    /*load the patient zero types namespace*/
    UA_StatusCode (* import_namespace)(UA_Server * )= load_namespace[0];
    retval = import_namespace(server);
    if(retval != UA_STATUSCODE_GOOD) {
        UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the pfdl_parameter namespace failed. Please check previous error output.");
        UA_Server_delete(server);
    }
    /*load the server specific namespace*/
    import_namespace = load_namespace[info->namespace_position];
    retval = import_namespace(server);
    if(retval != UA_STATUSCODE_GOOD) {
        UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the namespace on pos %zu in the load_namespace function failed. Please check previous error output.", info->namespace_position);
        UA_Server_delete(server);
    }
    /*load the corresponding server config*/
    UA_ByteString json = loadFile(info->path_to_config);
    UA_service_server_interpreter swap_server;
    memset(&swap_server, 0, sizeof(UA_service_server_interpreter));
    /* instantiate the resource specific subtype of the ModuleType*/
    UA_server_swap_it(server, json, info->methodcallback, UA_FALSE, info->running, UA_FALSE, &swap_server);
    UA_ByteString_clear(&json);
    /*start the server*/
    while(info->running) {
        UA_Server_run_iterate(server, true);
    }

    //clear memory
    clear_swap_server(&swap_server, UA_FALSE, server);
    /*Shut down the server*/
    UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_SERVER,"Shutting down server %s ", swap_server.server_name);
    UA_Server_run_shutdown(server);
    UA_Server_delete(server);

    pthread_exit((void *) info->threadId);
}

UA_Boolean running = true;
static void stopHandler(int sign) {
    UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "received ctrl-c");
    running = false;
}

int main() {
    signal(SIGINT, stopHandler);
    signal(SIGTERM, stopHandler);
    /* create the server structure*/
    size_t nbr_resources = 3;
    server_dict resources;
    resources.number_server = nbr_resources;
    resources.server = (server_info*) UA_calloc(nbr_resources, sizeof(server_info));
    /*configure the warehouse server*/
    char *path_to_warehouse_1_config = "../configs/warehouse_config.json5";
    resources.server[0].path_to_config = (char*) UA_calloc(strlen(path_to_warehouse_1_config+1), sizeof(char*));
    strcpy(resources.server[0].path_to_config, path_to_warehouse_1_config);
    resources.server[0].running = &running;
    resources.server[0].namespace_position = 1;
    resources.server[0].methodcallback = warehousemethodCallback;
    /*configure the coating server*/
    char *path_to_coating_1_config = "../configs/coating_config.json5";
    resources.server[1].path_to_config = (char*) UA_calloc(strlen(path_to_coating_1_config+1), sizeof(char*));
    strcpy(resources.server[1].path_to_config, path_to_coating_1_config);
    resources.server[1].running = &running;
    resources.server[1].namespace_position = 2;
    resources.server[1].methodcallback = coatingmethodCallback;
    /*configure the gluing server*/
    char *path_to_gluing_1_config = "../configs/gluing_config.json5";
    resources.server[2].path_to_config = (char*) UA_calloc(strlen(path_to_gluing_1_config+1), sizeof(char*));
    strcpy(resources.server[2].path_to_config, path_to_gluing_1_config);
    resources.server[2].running = &running;
    resources.server[2].namespace_position = 3;
    resources.server[2].methodcallback = gluingmethodCallback;

    /*start the server threads*/
    for(size_t i=0; i<resources.number_server; i++){
        pthread_create(&resources.server[i].threadId, NULL, start_server, &resources.server[i]);
    }
    while(running) {
        sleep(1);
    }
    for(size_t i=0; i<resources.number_server; i++){
        free(resources.server[i].path_to_config);
    }
    free(resources.server);
}