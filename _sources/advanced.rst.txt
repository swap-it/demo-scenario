..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)


=========
Advanced
=========

The advanced tutorial recreates the SWAP-IT Demonstration Scenario (See :ref: `Demonstration Scenario Process`. Consequently, the PFDL description is extended with the manufacture stand segment subprocess that includes a GetPartsFromWarehouse and a Milling service. To combine both subprocesses,
manufacture light segments and manufacture stand segment, the PDFL description completes the process with a Mounting service that mounts the light segments onto the stand segment. In addition, the assignment strategy differs to the intermediate and the beginner tutorial, so that this tutorial does not
feature a static resource assignment through a PFDL definition, instead it utilizes the dynamic resource assignment with a device registry and an assignment agent (see :ref:`Resource Assignment`).

Advanced: Writing custom PFDL files
===================================

The required structures remain the same compared to the beginner's and the intermediate's tutorial. However, since the assignment in accomplished with an assignment agent, the ResourceAssignment structure is no longer required.
Besides, the productionTask is extended with a Parallel condition with features on the one hand the manufacture_light_segments task, that is also defined in the intermediate's tutorial and on the other hand a new task, the manufacture_stand_segment task. Within this task,
a workpiece is taken from a warehouse and then processed by a milling machine to create a required shape for the stand. To complete the productionTask, a final Mounting task is added outside the Parallel condition, that mounts the light segments, produces in subprocess one, onto
the stand segment, produced in subprocess two.

.. code-block:: python

    Struct SWAP_Order
        order_id:number
        stand:Stand_Segment
        segments: Light_Segment[]
        number_light_segments: number
    End

    Struct Stand_Segment
        stand_shape:string
        stand_height:number
        stand_id:string
    End

    Struct Light_Segment
        color: string
        diameter: number
        segment_id:string
    End

    Task productionTask
        Parallel
            manufacture_light_segments
                In
                    SWAP_Order
                    {
                        "order_id":1000,
                        "stand":{
                            "stand_shape":"plate",
                            "stand_height":3,
                            "stand_id": "Default"
                        },
                        "segments":
                        [
                        {
                            "color": "red",
                            "diameter": 5,
                            "segment_id": "Default"
                        },
                        {
                            "color": "green",
                            "diameter": 5,
                            "segment_id": "Default"
                        }
                        ],
                        "number_light_segments": 5
                    }
            manufacture_stand_segment
                In
                    SWAP_Order
                        {
                            "order_id":1000,
                            "stand":{
                                "stand_shape":"plate",
                               "stand_height":3,
                                "stand_id": "Default"
                            },
                            "segments":
                            [
                            {
                                "color": "red",
                                "diameter": 5,
                                "segment_id": "Default"
                            },
                            {
                                "color": "green",
                                "diameter": 5,
                                "segment_id": "Default"
                            }
                            ],
                            "number_light_segments": 5
                        }
                Out
                    order:SWAP_Order
        Mounting
            In
                order
            Out
                order:SWAP_Order
    End


    Task manufacture_stand_segment
        In
            order: SWAP_Order
        GetPartsFromWarehouse
            In
                order
            Out
                order: SWAP_Order
        Milling
            In
                order
            Out
                order:SWAP_Order
        Out
            order
    End

    Task manufacture_light_segments
        In
            order: SWAP_Order
        Parallel Loop i To order.number_light_segments
            manufacture_light_segment
                In
                    order
                Out
                    order:SWAP_Order
        Loop i To order.number_light_segments
            Gluing
                In
                    order
                Out
                    order:SWAP_Order
    End

    Task manufacture_light_segment
        In
            order: SWAP_Order
        GetPartsFromWarehouse
            In
                order
            Out
                order: SWAP_Order
        Coating
            In
                order
            Out
                order: SWAP_Order
        Out
            order
    End





The resulting visual representation of the process is shown below:

.. figure:: /images/petri_net.PNG
   :alt: alternate text


Advanced: Building Resource-specific Information Models
======================================================================



The pre-build *Model.Nodeset2.xml* files for each namespace in this tutorial can be found within the directory `InformationModels <https://github.com/swap-it/demo-scenario/tree/main/Tutorials/advanced/model>`_.
However, if it is  planned to use the pre-build files, this section can be skipped and the tutorial continues with section `Advanced Build custom OPC UA Servers`_, however the following sections give a short introduction on how to
define *.ModelDesign.xml* files, which can then be processed with the `UA-ModelCompiler <https://github.com/OPCFoundation/UA-ModelCompiler>`_ to generate *Model.Nodeset2.xml* files.


.. toctree::
   :maxdepth: 2

   pfdl_namespace
   warehouse_namespace
   coating_namespace
   gluing_namespace
   milling_namespace
   mounting_namespace

.. _Advanced Build custom OPC UA Servers:
Advanced: Build custom OPC UA Servers
=========================================
Similar to the intermediate tutorial, all servers will be started from one executable based on thread. To start several resource server from the same script,
the following example uses a thread based server start. This is fine for a simulation purpose, however, when connecting the servers to real physical resources, it
is recommended to implement each server in an individual application. The following gives a short introduction to such a thread based shop floor configuration.

In addition, the shop floor is extended to prevent bottle necks, so that instead of a single coating and a single warehouse module, the number of these modules is increased to three and two respectively.

.. _Advanced Writing a JSON-configuration File:

Advanced: Writing a JSON-configuration File
===========================================
JSON files are required for each of the server, so that an overall number of 8 JSON configurations are defined. Since the dynamic resource assignment is used, minimal configuration is extended with the key device_registry_url

Warehouse Configuration
------------------------

Warehouse 1
............

.. code-block:: c

    {
      //mandatory
      application_name: "warehouse_dr1",
      resource_ip: "localhost",
      port: "4081",
      module_type: "WarehouseModuleType",
      module_name: "WarehouseModule",
      service_name: "GetPartsFromWarehouse",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

Warehouse 2
............

.. code-block:: c

    {
      //mandatory
      application_name: "warehouse_dr2",
      resource_ip: "localhost",
      port: "4082",
      module_type: "WarehouseModuleType",
      module_name: "WarehouseModule",
      service_name: "GetPartsFromWarehouse",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

Coating Configuration
---------------------

Coating 1
............

.. code-block:: c

    {
      //mandatory
      application_name: "coating_dr1",
      resource_ip: "localhost",
      port: "4091",
      module_type: "CoatingModuleType",
      module_name: "CoatingModule",
      service_name: "Coating",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

Coating 2
............

.. code-block:: c

    {
      //mandatory
      application_name: "coating_dr2",
      resource_ip: "localhost",
      port: "4092",
      module_type: "CoatingModuleType",
      module_name: "CoatingModule",
      service_name: "Coating",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

Coating 3
............

.. code-block:: c

    {
      //mandatory
      application_name: "coating_dr3",
      resource_ip: "localhost",
      port: "4093",
      module_type: "CoatingModuleType",
      module_name: "CoatingModule",
      service_name: "Coating",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

Gluing Configuration
---------------------

.. code-block:: c

    {
      //mandatory
      application_name: "gluing_dr1",
      resource_ip: "localhost",
      port: "4061",
      module_type: "GluingModuleType",
      module_name: "GluingModule",
      service_name: "Gluing",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

Mounting Configuration
-----------------------

.. code-block:: c

    {
      //mandatory
      application_name: "mounting_dr1",
      resource_ip: "localhost",
      port: "4051",
      module_type: "MountingModuleType",
      module_name: "MountingModule",
      service_name: "Mounting",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

Milling Configuration
----------------------

.. code-block:: c

    {
      //mandatory
      application_name: "milling_dr1",
      resource_ip: "localhost",
      port: "4071",
      module_type: "MillingModuleType",
      module_name: "MillingModule",
      service_name: "Milling",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }


Advanced: CMake Configuration
=============================

Since all server application use the same installation of the open62541 OPC UA SDK, it is possible to extend the C-Make configuration,
so that the nodeset-compiler creates c-code from all information models. Besides it is necessary to link the library that is required for the threads.

.. code-block:: c

    cmake_minimum_required(VERSION 3.0.0)
    project(swap_server)
    find_package(open62541 1.3 REQUIRED)

    if(NOT CMAKE_PROJECT_NAME STREQUAL PROJECT_NAME)
        # needed or cmake doesn't recognize dependencies of generated files
        set(PROJECT_BINARY_DIR ${CMAKE_BINARY_DIR})
    endif()

    file(MAKE_DIRECTORY "${GENERATE_OUTPUT_DIR}")
    set(GENERATE_OUTPUT_DIR "${CMAKE_BINARY_DIR}/src_generated/")
    include_directories("${GENERATE_OUTPUT_DIR}")
    set(INFORMATION_MODEL_DIR ${PROJECT_SOURCE_DIR}/Information_Models)


    ua_generate_nodeset_and_datatypes(
            NAME "common"
            FILE_BSD "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            FILE_CSV "${INFORMATION_MODEL_DIR}/CommonModelDesign.csv"
            FILE_NS "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Common.Model.NodeSet2.xml"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "pfdl_parameter"
            FILE_BSD "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Model.Types.bsd"
            FILE_CSV "${INFORMATION_MODEL_DIR}/PatientZeroDataTypesModelDesign.csv"
            FILE_NS "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Model.NodeSet2.xml"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "warehouse"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Warehouse.Model.Types.bsd"
            FILE_CSV "${INFORMATION_MODEL_DIR}/PatientZeroWarehouseModelDesign.csv"
            FILE_NS "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Warehouse.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Model.Types.bsd"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "coating"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Coating.Model.Types.bsd"
            FILE_CSV "${INFORMATION_MODEL_DIR}/CoatingModelDesign.csv"
            FILE_NS "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Coating.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Model.Types.bsd"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "milling"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Milling.Model.Types.bsd"
            FILE_CSV "${INFORMATION_MODEL_DIR}/MillingModelDesign.csv"
            FILE_NS "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Milling.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Model.Types.bsd"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "gluing"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Gluing.Model.Types.bsd"
            FILE_CSV "${INFORMATION_MODEL_DIR}/GluingModelDesign.csv"
            FILE_NS "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Gluing.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Model.Types.bsd"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "mounting"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Mounting.Model.Types.bsd"
            FILE_CSV "${INFORMATION_MODEL_DIR}/MountingModelDesign.csv"
            FILE_NS "${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Mounting.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${INFORMATION_MODEL_DIR}/SWAP.Fraunhofer.Patient.Zero.Model.Types.bsd"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    add_executable(swap_server
            main.c
            custom_behavior/service_callbacks.c
            ${UA_NODESET_PFDL_PARAMETER_SOURCES}
            ${UA_TYPES_PFDL_PARAMETER_SOURCES}
            ${UA_NODESET_COMMON_SOURCES}
            ${UA_TYPES_COMMON_SOURCES}
            ${UA_NODESET_WAREHOUSE_SOURCES}
            ${UA_TYPES_WAREHOUSE_SOURCES}
            ${UA_NODESET_COATING_SOURCES}
            ${UA_TYPES_COATING_SOURCES}
            ${UA_NODESET_MILLING_SOURCES}
            ${UA_TYPES_MILLING_SOURCES}
            ${UA_NODESET_GLUING_SOURCES}
            ${UA_TYPES_GLUING_SOURCES}
            ${UA_NODESET_MOUNTING_SOURCES}
            ${UA_TYPES_MOUNTING_SOURCES}
            )

    add_dependencies(swap_server open62541-generator-ns-common
                                 open62541-generator-ns-pfdl_parameter
                                 open62541-generator-ns-warehouse
                                 open62541-generator-ns-coating
                                 open62541-generator-ns-milling
                                 open62541-generator-ns-gluing
                                 open62541-generator-ns-mounting
            )
    target_link_libraries(swap_server swap_server_template)
    target_link_libraries(swap_server open62541::open62541)
    target_link_libraries(swap_server pthread)

Advanced: Build Server
======================
For the eight servers, the open62541-server template is used and the first step is the definition of a method to load the corresponding JSON configuration files.

.. code-block:: c

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

Since each of the different server requires a custom method callback, an additional directory, custom_behavior is defined that includes a header and a source file. In this source file, each of the method callbacks for the different server are defined.

**Header File**

Within the header file, the event for each method is called from a separate thread, to enable a sincronous methodcallback and an asynchronous event, that indicates the completion of the service execution. Since this thread only browses the corresponding server
for the eventtype and then, triggers the event, this thread uses the function find_service_event from the open62541 swap server template.

.. code-block:: c


    #ifndef OPEN62541_SERVICE_CALLBACKS_H
    #define OPEN62541_SERVICE_CALLBACKS_H
    #include <open62541/plugin/log_stdout.h>
    #include "stdio.h"

    UA_StatusCode warehousemethodCallback(UA_Server *server,
                                          const UA_NodeId *sessionId, void *sessionHandle,
                                          const UA_NodeId *methodId, void *methodContext,
                                          const UA_NodeId *objectId, void *objectContext,
                                          size_t inputSize, const UA_Variant *input,
                                          size_t outputSize, UA_Variant *output);

    UA_StatusCode coatingmethodCallback(UA_Server *server,
                                          const UA_NodeId *sessionId, void *sessionHandle,
                                          const UA_NodeId *methodId, void *methodContext,
                                          const UA_NodeId *objectId, void *objectContext,
                                          size_t inputSize, const UA_Variant *input,
                                          size_t outputSize, UA_Variant *output);

    UA_StatusCode millingmethodCallback(UA_Server *server,
                                        const UA_NodeId *sessionId, void *sessionHandle,
                                        const UA_NodeId *methodId, void *methodContext,
                                        const UA_NodeId *objectId, void *objectContext,
                                        size_t inputSize, const UA_Variant *input,
                                        size_t outputSize, UA_Variant *output);

    UA_StatusCode gluingmethodCallback(UA_Server *server,
                                       const UA_NodeId *sessionId, void *sessionHandle,
                                       const UA_NodeId *methodId, void *methodContext,
                                       const UA_NodeId *objectId, void *objectContext,
                                       size_t inputSize, const UA_Variant *input,
                                       size_t outputSize, UA_Variant *output);

    UA_StatusCode mountingmethodCallback(UA_Server *server,
                                         const UA_NodeId *sessionId, void *sessionHandle,
                                         const UA_NodeId *methodId, void *methodContext,
                                         const UA_NodeId *objectId, void *objectContext,
                                         size_t inputSize, const UA_Variant *input,
                                         size_t outputSize, UA_Variant *output);

    #endif  // OPEN62541_SERVICE_CALLBACKS_H

**Source File**


.. code-block:: c

    #include <unistd.h>
    #include <open62541/plugin/log_stdout.h>
    #include <open62541/server.h>

    #include "types_common_generated_handling.h"
    #include "types_pfdl_parameter_generated.h"
    #include "service_callbacks.h"
    #include "node_finder.h"

    /*define structures for the event thread*/
    typedef struct {
        UA_String service_name;
        UA_Server *server;
        pthread_t threadId;
    }UA_simple_service_handler;

    /*function to call the event*/
    static void *execute_event_with_order_result(void *ptr){
        UA_simple_service_handler *service_dict = (UA_simple_service_handler*) ptr;
        UA_String out = UA_STRING_NULL;
        UA_print(&service_dict->service_name, &UA_TYPES[UA_TYPES_STRING], &out);
        printf("Trigger the event of service : %.*s\n", (int)out.length, out.data);
        UA_String_clear(&out);

        UA_NodeId event_nodeId;
        UA_NodeId_init(&event_nodeId);
        //browse the server for the service finished event type
        find_service_event(service_dict->server, &event_nodeId, service_dict->service_name);
        UA_print(&event_nodeId, &UA_TYPES[UA_TYPES_NODEID], &out);
        printf("ServiceEvent NodeId : %.*s\n", (int)out.length, out.data);
        UA_String_clear(&out);
        UA_String_clear(&service_dict->service_name);

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

        UA_print(&order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER], &out);
        printf("Order Variable: %.*s\n", (int)out.length, out.data);
        UA_String_clear(&out);

        UA_Variant temp;
        UA_Variant_init(&temp);
        UA_Variant_setScalarCopy(&temp, &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);

        //create the event
        UA_NodeId eventOutNodeId;
        UA_NodeId_init(&eventOutNodeId);
        UA_Server_createEvent(service_dict->server, event_nodeId, &eventOutNodeId);
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Trigger the event");
        //write the output value to the event
        UA_Server_writeObjectProperty_scalar(service_dict->server, eventOutNodeId,
                                             UA_QUALIFIEDNAME(4, "order"), &order, &UA_TYPES_PFDL_PARAMETER[UA_TYPES_PFDL_PARAMETER_SWAP_ORDER]);
        UA_Server_triggerEvent(service_dict->server, eventOutNodeId, UA_NODEID_NUMERIC(0, UA_NS0ID_SERVER), NULL, UA_TRUE);
        //clear allocated memory
        UA_NodeId_clear(&eventOutNodeId);
        UA_NodeId_clear(&event_nodeId);
        free(service_dict);
        free(order.segments);

        return UA_STATUSCODE_GOOD;
    }

    /*method callbacks for the corresponding service methods*/
    UA_StatusCode warehousemethodCallback(UA_Server *server,
                                                 const UA_NodeId *sessionId, void *sessionHandle,
                                                 const UA_NodeId *methodId, void *methodContext,
                                                 const UA_NodeId *objectId, void *objectContext,
                                                 size_t inputSize, const UA_Variant *input,
                                                 size_t outputSize, UA_Variant *output){

        UA_String service_name = UA_String_fromChars("GetPartsFromWarehouse");
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Service execution Started");
        UA_simple_service_handler *handler = UA_calloc(1, sizeof(UA_simple_service_handler));
        UA_String_copy(&service_name, &handler->service_name);
        handler->server = server;
        pthread_create(&handler->threadId, NULL, execute_event_with_order_result, handler);
        UA_String_clear(&service_name);

        //create the ServiceExecutionAsyncResultDataType variable
        UA_ServiceExecutionAsyncResultDataType res;
        UA_ServiceExecutionAsyncResultDataType_init(&res);
        res.serviceResultCode = (UA_UInt32) 1;
        res.serviceResultMessage = UA_String_fromChars("Message");
        res.expectedServiceExecutionDuration = (UA_Double) 7;
        res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;

        UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
        UA_String_clear(&res.serviceResultMessage);
        return UA_STATUSCODE_GOOD;
    }

    UA_StatusCode coatingmethodCallback(UA_Server *server,
                                          const UA_NodeId *sessionId, void *sessionHandle,
                                          const UA_NodeId *methodId, void *methodContext,
                                          const UA_NodeId *objectId, void *objectContext,
                                          size_t inputSize, const UA_Variant *input,
                                          size_t outputSize, UA_Variant *output){

        UA_String service_name = UA_String_fromChars("Coating");
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Service execution Started");
        UA_simple_service_handler *handler = UA_calloc(1, sizeof(UA_simple_service_handler));
        UA_String_copy(&service_name, &handler->service_name);
        handler->server = server;
        pthread_create(&handler->threadId, NULL, execute_event_with_order_result, handler);
        UA_String_clear(&service_name);

        //create the ServiceExecutionAsyncResultDataType variable
        UA_ServiceExecutionAsyncResultDataType res;
        UA_ServiceExecutionAsyncResultDataType_init(&res);
        res.serviceResultCode = (UA_UInt32) 1;
        res.serviceResultMessage = UA_String_fromChars("Message");
        res.expectedServiceExecutionDuration = (UA_Double) 7;
        res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;

        UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
        UA_String_clear(&res.serviceResultMessage);
        return UA_STATUSCODE_GOOD;
    }

    UA_StatusCode millingmethodCallback(UA_Server *server,
                                        const UA_NodeId *sessionId, void *sessionHandle,
                                        const UA_NodeId *methodId, void *methodContext,
                                        const UA_NodeId *objectId, void *objectContext,
                                        size_t inputSize, const UA_Variant *input,
                                        size_t outputSize, UA_Variant *output){

        UA_String service_name = UA_String_fromChars("Milling");
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Service execution Started");
        UA_simple_service_handler *handler = UA_calloc(1, sizeof(UA_simple_service_handler));
        UA_String_copy(&service_name, &handler->service_name);
        handler->server = server;
        pthread_create(&handler->threadId, NULL, execute_event_with_order_result, handler);
        UA_String_clear(&service_name);

        //create the ServiceExecutionAsyncResultDataType variable
        UA_ServiceExecutionAsyncResultDataType res;
        UA_ServiceExecutionAsyncResultDataType_init(&res);
        res.serviceResultCode = (UA_UInt32) 1;
        res.serviceResultMessage = UA_String_fromChars("Message");
        res.expectedServiceExecutionDuration = (UA_Double) 7;
        res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;

        UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
        UA_String_clear(&res.serviceResultMessage);
        return UA_STATUSCODE_GOOD;
    }

    UA_StatusCode gluingmethodCallback(UA_Server *server,
                                        const UA_NodeId *sessionId, void *sessionHandle,
                                        const UA_NodeId *methodId, void *methodContext,
                                        const UA_NodeId *objectId, void *objectContext,
                                        size_t inputSize, const UA_Variant *input,
                                        size_t outputSize, UA_Variant *output){

        UA_String service_name = UA_String_fromChars("Gluing");
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Service execution Started");
        UA_simple_service_handler *handler = UA_calloc(1, sizeof(UA_simple_service_handler));
        UA_String_copy(&service_name, &handler->service_name);
        handler->server = server;
        pthread_create(&handler->threadId, NULL, execute_event_with_order_result, handler);
        UA_String_clear(&service_name);

        //create the ServiceExecutionAsyncResultDataType variable
        UA_ServiceExecutionAsyncResultDataType res;
        UA_ServiceExecutionAsyncResultDataType_init(&res);
        res.serviceResultCode = (UA_UInt32) 1;
        res.serviceResultMessage = UA_String_fromChars("Message");
        res.expectedServiceExecutionDuration = (UA_Double) 7;
        res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;

        UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
        UA_String_clear(&res.serviceResultMessage);
        return UA_STATUSCODE_GOOD;
    }

    UA_StatusCode mountingmethodCallback(UA_Server *server,
                                       const UA_NodeId *sessionId, void *sessionHandle,
                                       const UA_NodeId *methodId, void *methodContext,
                                       const UA_NodeId *objectId, void *objectContext,
                                       size_t inputSize, const UA_Variant *input,
                                       size_t outputSize, UA_Variant *output){

        UA_String service_name = UA_String_fromChars("Mounting");
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_USERLAND, "Service execution Started");
        UA_simple_service_handler *handler = UA_calloc(1, sizeof(UA_simple_service_handler));
        UA_String_copy(&service_name, &handler->service_name);
        handler->server = server;
        pthread_create(&handler->threadId, NULL, execute_event_with_order_result, handler);
        UA_String_clear(&service_name);

        //create the ServiceExecutionAsyncResultDataType variable
        UA_ServiceExecutionAsyncResultDataType res;
        UA_ServiceExecutionAsyncResultDataType_init(&res);
        res.serviceResultCode = (UA_UInt32) 1;
        res.serviceResultMessage = UA_String_fromChars("Message");
        res.expectedServiceExecutionDuration = (UA_Double) 7;
        res.serviceTriggerResult = UA_SERVICETRIGGERRESULT_SERVICE_RESULT_ACCEPTED;

        UA_Variant_setScalarCopy(output, &res, &UA_TYPES_COMMON[UA_TYPES_COMMON_SERVICEEXECUTIONASYNCRESULTDATATYPE]);
        UA_String_clear(&res.serviceResultMessage);
        return UA_STATUSCODE_GOOD;
    }

Next, going back to the main.c file, two structures are defined from which each server configuration will be handed over to the corresponding server thread.
The structure server_info contains the path to the JSON configuration file and the method callback of the corresponding server. Since a generic function for the
server start inside each thread is used, it is not possible to preset the function with which the namespaces are loaded. To solve this problem, a list of pointers to
the corresponding load namespace functions is provided (* load_namespace). The index of the corresponding function is stored within the server_info structure. Lastly, the structure
server_dict contains a list with all server configurations, so that all threads can be started from a loop over the server_dict.number_server variable.

.. code-block::

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

In the next step, the generic start_server function is defined, which is executed within each server thread and starts the corresponding OPC UA server. In contrast to the previous tutorials, the UA_Boolean register_agent_in_registry in the UA_Server_swap_it
function and the argument UA_Boolean unregister in the clear_swap_server functions are set to UA_TRUE.

.. code-block:: c

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
        if(retval != UA_STATUSCODE_GOOD){
            UA_ByteString_clear(&json);
            pthread_exit((void *) info->threadId);
        }else{
            UA_ByteString_clear(&json);
            UA_Server_run_startup(server);
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

Finally, it is possible to define the main function from which all treads are started.

.. code-block:: c

    UA_Boolean running = true;
    static void stopHandler(int sign) {
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "received ctrl-c");
        running = false;
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


Advanced: Build the Server Executable
--------------------------------------------
After the code is completed, it is now possible to compile the server executable. For this to be achieved, create a build directory within the working directory and compile the server with the cmake and make command.

.. code-block:: c

    cd working directory
    mkdir build && cd build
    cmake ..
    make
    /*start the executable*/
    ./swap_server

**Important note:**
Since the server try to register themselves within a device registry, it has to be ensured that a corresponding instance of a device registry is running.

Advanced: Execute the PFDL with a Process Agent
==========================================================
Since the advanced tutorial features a dynamic resource assignment, an instance of a device registry has to be started to make the server available to the system.


Start the Application with Docker Images
-----------------------------------------
An easy approach to start the application and start processes on the servers created within this tutorial are docker container that are available within the GitHub package registry. The application ca be started in 3 steps,
first the docker container of the device registry has to be started. Second, the shop floor build within this tutorial has to be started. In the last step, the process agent is started, which then executes the PFDL of the Demonstration Scenario.

Step 1: Start the Device Registry
.................................
For the device registry, a pre-build docker container is available which can be started with:

.. code-block:: c

    docker run -p 8000:8000 --add-host host.docker.internal:host-gateway ghcr.io/swap-it/demo-scenario/device_registry:latest

Step 2: Start the Shop Floor Server
....................................
Since docker container have an individual networking, a minor modification on the JSON configuration files has to be performed to enable the connection between the docker container and the shop floor servers.
Here, the key **resource_ip** has to be changed to resource_ip: "host.docker.internal".

The configuration file for the milling server would then look like this:

.. code-block:: c

    {
      //mandatory
      application_name: "milling_dr1",
      resource_ip: "host.docker.internal",
      port: "4071",
      module_type: "MillingModuleType",
      module_name: "MillingModule",
      service_name: "Milling",
      //optional
      device_registry:"opc.tcp://localhost:8000"
    }

However, this step has to be repeated for all configurations that are defined in section `Advanced Writing a JSON-configuration File`_.

In case of an individual implementation of this tutorial, the shop floor can be started with the following steps:

.. code-block:: c

    cd working directory
    mkdir build && cd build
    cmake ..
    make
    /*start the executable*/
    ./swap_server

In case that the shop floor should be started from the tutorial implementation, execute the following steps:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario.git
    cd demo-scenario/Tutorials/advanced
    mkdir build && cd build
    cmake ..
    make
    /*start the executable*/
    ./swap_server


Step 3: Start the Process Agent
...............................
Since the code basis of the process agent in not available yet, we provide a docker compose file which starts the missing application, in fact the process agent and the dashboard, in a docker compose project. A corresponding
Docker-compose.yaml file is provided in the `advanced directory <https://github.com/swap-it/demo-scenario/tree/main/Tutorials/advanced>`_.
This docker application can be started with the following steps, so that the beginner tutorial PFDL and the corresponding process is executed on the resource, which is implemented in this tutorial:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario.git
    cd demo-scenario/Tutorials/advanced
    docker-compose up