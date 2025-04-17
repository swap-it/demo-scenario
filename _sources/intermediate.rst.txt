..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)

=============
Intermediate
=============

This tutorial extends the beginners tutorial by not only executing the GetPartsFromWarehouse service, but also
the Coating and the Gluing service. As these
services are performed on a set of parts of the traffic light, the PFDL description is
extended with a Parallel Loop for the GetPartsFromWarehouse- and Coating service, and a Counting Loop
for the Gluing service.


Intermediate: Writing custom PFDL files
=======================================

Since all required PFDL structures were already defined in the beginners tutorial (:ref:`Beginner Writing custom PFDL files`),
only the productionTask task is extended with the additional services and conditions.
To have a better structure inside the PFDL, additional tasks are defined around these services. First, the manufacture_light_segments task is executed from the productionTask and contains and invokes the an additional task, the manufacture_light_segment task.
Within the manufacture_light_segment task a part is taken from the warehouse and coated in the color that is specified within the corresponding Light_Segment structure. To ensure that all light_segment are available when staring the
Gluing service. A Parallel Loop is defined in which each of the manufacture_light_segment tasks is executed. The use of the Parallel Loop has two reasons, first the loop enables a flexible number of iterations over the corresponding task,
and second, the parallel condition ensures that the process does not transition into the Gluing service, before all light segments finished the previous manufacturing step. The number of iterations is determined by the number_light_segments field of the
SWAP_Order structure. Since the next step glues all light segments together, a counting loop is used in which the Gluing service is executed. This counting loop is also based on the number_light_segments field of the SWAP_Order structure.


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

    #structure to statically assign the service to a resource
    #this structure is only uses by the execution engine internally,
    #so it does not have to be defined
    #within OPC UA namespace for the warehouse server
    Struct ResourceAssignment
            job_resource:string
    End

    Task productionTask
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
                            "color": "yellow",
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
    End

    Task manufacture_light_segments
        In
            order: SWAP_Order
        #parallel loop based on the field number_light_segments
        #of the SWAP_Order structure
        Parallel Loop i To order.number_light_segments
            manufacture_light_segment
                In
                    order
        #counting loop based on the field number_light_segments
        #of the SWAP_Order structure
        Loop i To order.number_light_segments
            Gluing
                In
                    ResourceAssignment
                    {
                        "job_resource":"opc.tcp://host.docker.internal:4841"
                    }
                    order
                Out
                    order: SWAP_Order
    End


    Task manufacture_light_segment
        In
            order: SWAP_Order
        GetPartsFromWarehouse
            In
                ResourceAssignment
                    {
                        "job_resource":"opc.tcp://host.docker.internal:4840"
                    }
                order
            Out
               order: SWAP_Order
        Coating
            In
                ResourceAssignment
                    {
                        "job_resource":"opc.tcp://host.docker.internal:4842"
                    }
                order
            Out
                order: SWAP_Order
    End

The resulting visual representation of the process is shown below:

.. figure:: /images/intermediate.PNG
   :alt: Process Visualization Beginner
   :width: 100%

.. _Intermediate Building Resource-specific Information Models:

Intermediate: Building Resource-specific Information Models
======================================================================

The pre-build Nodeset2.xml files for each namespace in this tutorial can be found within the directory `InformationModels <https://github.com/swap-it/demo-scenario/tree/main/Tutorials/intermediate/model>`_.
However, if it is  planned to use the pre-build files, this section can be skipped and the tutorial continues with section `Intermediate Build custom OPC UA Servers`_, however the following sections give a short introduction on how to
define *.ModelDesign.xml files, which can then be processed with the `UA-ModelCompiler <https://github.com/OPCFoundation/UA-ModelCompiler>`_ to generate NodeSet2.xml files.

.. toctree::
   :maxdepth: 4

   pfdl_namespace
   warehouse_namespace
   coating_namespace
   gluing_namespace


.. _Intermediate Build custom OPC UA Servers:

Intermediate: Build custom OPC UA Servers
=========================================

To start several resource server from the same script, the following example uses a thread based server start. This is fine for a simulation purpose, however, when connecting the
servers to real physical resources, it is recommended to implement each server in an individual application. The following gives a short introduction to such a thread based shop floor configuration.

Intermediate: Writing a JSON-configuration file
----------------------------------------------------------

Since three servers are used, the definition of a JSON-configuration file for each of the server is required.

Warehouse Configuration
~~~~~~~~~~~~~~~~~~~~~~~

.. code-block:: c

        {
          application_name: "warehouse",
          resource_ip: "localhost",
          port: "4840",
          module_type: "WarehouseModuleType",
          module_name: "WarehouseModule",
          service_name: "GetPartsFromWarehouse"
        }

Coating Configuration
~~~~~~~~~~~~~~~~~~~~~~~

.. code-block:: c

    {
      application_name: "coating_dr1",
      resource_ip: "localhost",
      port: "4842",
      module_type: "CoatingModuleType",
      module_name: "CoatingModule",
      service_name: "Coating"
    }

Gluing Configuration
~~~~~~~~~~~~~~~~~~~~~~~

.. code-block:: c

   {
      application_name: "gluing_dr1",
      resource_ip: "localhost",
      port: "4841",
      module_type: "GluingModuleType",
      module_name: "GluingModule",
      service_name: "Gluing"
    }


Intermediate: CMake Configuration
---------------------------------

Since all server application use the same installation of the open62541 OPC UA SDK, it is possible to extend the C-Make configuration, so that the
nodeset-compiler creates c-code from all information models. Besides it is necessary to link the library that is required for the threads.


.. code-block:: c

    cmake_minimum_required(VERSION 3.0.0)
    #set name of the CMakeProject
    project(intermediate_tutorial)
    find_package(open62541 1.3 REQUIRED)
    if(NOT CMAKE_PROJECT_NAME STREQUAL PROJECT_NAME)
        # needed or cmake doesn't recognize dependencies of generated files
        set(PROJECT_BINARY_DIR ${CMAKE_BINARY_DIR})
    endif()
    #define the path to the directory in which the
    set(PATH_TO_INFORMATION_MODELS ${PROJECT_SOURCE_DIR}/model)
    #set output directory
    set(GENERATE_OUTPUT_DIR "${CMAKE_BINARY_DIR}/src_generated/")
    include_directories("${GENERATE_OUTPUT_DIR}")
    #generate c code from the common model
    ua_generate_nodeset_and_datatypes(
            NAME "common"
            FILE_BSD "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            FILE_CSV "${PATH_TO_INFORMATION_MODELS}/CommonModelDesign.csv"
            FILE_NS "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Common.Model.NodeSet2.xml"
            NAMESPACE_MAP "2:http://common.swap.fraunhofer.de"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )
    #generate c code from the pfdl types model
    ua_generate_nodeset_and_datatypes(
            NAME "pfdl_parameter"
            FILE_BSD "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Model.Types.bsd"
            FILE_CSV "${PATH_TO_INFORMATION_MODELS}/DemoScenarioTypes.ModelDesign.csv"
            FILE_NS "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Model.NodeSet2.xml"
            NAMESPACE_MAP "3:http://swap.demo.scenario.fraunhofer.de"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )
    #generate c code from the warehouse model and set the dependencies to the common and pfdl types models
    ua_generate_nodeset_and_datatypes(
            NAME "warehouse"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Warehouse.Model.Types.bsd"
            FILE_CSV "${PATH_TO_INFORMATION_MODELS}/DemoScenarioWarehouse.ModelDesign.csv"
            FILE_NS "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Warehouse.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Model.Types.bsd"
            NAMESPACE_MAP "4:http://swap.demo.scenario.warehouse.fraunhofer.de"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "coating"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Coating.Model.Types.bsd"
            FILE_CSV "${PATH_TO_INFORMATION_MODELS}/DemoScenarioCoating.ModelDesign.csv"
            FILE_NS "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Coating.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Model.Types.bsd"
            NAMESPACE_MAP "4:http://swap.demo.scenario.coating.fraunhofer.de"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    ua_generate_nodeset_and_datatypes(
            NAME "gluing"
            DEPENDS "common"
            DEPENDS "pfdl_parameter"
            FILE_BSD "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Gluing.Model.Types.bsd"
            FILE_CSV "${PATH_TO_INFORMATION_MODELS}/DemoScenarioGluing.ModelDesign.csv"
            FILE_NS "${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Gluing.Model.NodeSet2.xml"
            IMPORT_BSD "TYPES_COMMON#${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Common.Model.Types.bsd"
            IMPORT_BSD "TYPES_PFDL_PARAMETER#${PATH_TO_INFORMATION_MODELS}/SWAP.Fraunhofer.Demo.Scenario.Model.Types.bsd"
            NAMESPACE_MAP "4:http://swap.demo.scenario.gluing.fraunhofer.de"
            OUTPUT_DIR ${GENERATE_OUTPUT_DIR}
            INTERNAL
    )

    #adds the executable target
    add_executable(intermediate_tutorial
            intermediate.c
            ${UA_NODESET_PFDL_PARAMETER_SOURCES}
            ${UA_TYPES_PFDL_PARAMETER_SOURCES}
            ${UA_NODESET_COMMON_SOURCES}
            ${UA_TYPES_COMMON_SOURCES}
            ${UA_NODESET_WAREHOUSE_SOURCES}
            ${UA_TYPES_WAREHOUSE_SOURCES}
            ${UA_NODESET_COATING_SOURCES}
            ${UA_TYPES_COATING_SOURCES}
            ${UA_NODESET_GLUING_SOURCES}
            ${UA_TYPES_GLUING_SOURCES}
    )

    #add the dependencies between the beginner tutorial and the generated namspaces
    add_dependencies(intermediate_tutorial  open62541-generator-ns-common
                                            open62541-generator-ns-pfdl_parameter
                                            open62541-generator-ns-warehouse
                                            open62541-generator-ns-coating
                                            open62541-generator-ns-gluing
    )
    #link the beginner tutorial project with the installed open62541 server template
    target_link_libraries(intermediate_tutorial swap_server_template)
    #link the beginner tutorial project with the installed open62541 SDK
    target_link_libraries(intermediate_tutorial open62541::open62541)
    target_link_libraries(intermediate_tutorial pthread)

Intermediate: Build Server
---------------------------------
For the three servers, the open62541-server template is used and the first step is the definition of a method to load the corresponding JSON configuration files.

.. code-block:: c

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

As next step, the callback functions for the GetPartsFromWarehouse, Gluing and Coating OPC UA methods are defined.

.. code-block:: c

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

Next, two structures are defined from which each server configuration will be handed over to the corresponding server thread.
The structure server_info contains the path to the JSON configuration file and the method callback of the corresponding server. Since a generic function for the
server start inside each thread is used, it is not possible to preset the function with which the namespaces are loaded. To solve this problem, a list of pointers to
the corresponding load namespace functions is provided (* load_namespace). The index of the corresponding function is stored within the server_info structure. Lastly, the structure
server_dict contains a list with all server configurations, so that all threads can be started from a loop over the server_dict.number_server variable.


.. code-block:: c

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

In the next step, the generic start_server function is defined, which is executed within each server thread and starts the corresponding OPC UA server.

.. code-block:: c

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
        UA_Server_run_startup(server);
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


Intermediate: Build the Server Executable
--------------------------------------------
After the code is completed, it is now possible to compile the server executable. For this to be achieved, create a build directory within the working directory and compile the server with the cmake and make command.

.. code-block:: c

    cd working directory
    mkdir build && cd build
    cmake ..
    make
    /*start the executable*/
    ./intermediate_tutorial


In case that the shop floor should be started from the tutorial implementation, execute the following steps:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario.git
    cd demo-scenario/Tutorials/intermediate
    mkdir build && cd build
    cmake ..
    make
    /*start the executable*/
    ./intermediate_tutorial

Intermediate: Execute the PFDL with a Process Agent
===================================================
Two approaches are possible to test the server of the beginner tutorial with a process agent. First, the process Agent Repository can be cloned and locally executed, or a pre-build docker-environment can be deployed.

Intermediate: Start from Docker
-------------------------------


For the Docker approach, an image of the process agent and the dashboard are provided. A corresponding
Docker-compose.yaml file is provided in the `intermediate directory <https://github.com/swap-it/demo-scenario/tree/main/Tutorials/intermediate>`_.
This docker application can be started with the following steps, so that the beginner tutorial PFDL and the corresponding process is executed on the resource, which is implemented in this tutorial:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario.git
    cd demo-scenario/Tutorials/intermediate
    docker-compose up


Intermediate: Start from Scratch
--------------------------------
In this section, both the `swap-it-dashboard <https://github.com/iml130/swap-it-dashboard>`_ and the `swap-it-process-agent <https://github.com/FraunhoferIOSB/swap-it-execution-engine>`_ are locally executed. Please consider
the corresponding installation requirement of the corresponding code bases.

The swap-it-dashboard can be simply started with:

.. code-block:: python

    #clone the repository
    git clone https://github.com/iml130/swap-it-dashboard.git
    cd swap-it-dashboard
    python3 application.py

Then the swap-it-dashboard can be opened with **http://localhost:8080**

Next, a process agent can be launched:

.. code-block:: python

    #clone the repository
    https://github.com/FraunhoferIOSB/swap-it-execution-engine
    cd swap-it-execution-engine
    python3 main.py "opc.tcp://localhost:4844" "./PFDL_Examples/intermediate.pfdl" "dashboard_host_address"="http://localhost:8080" "log_info"=True "number_default_clients"=5

Please consider to adjust the ResourceAssignment Structure within the PFDL description as followed:

.. code-block:: json

    ResourceAssignment
    {
        "job_resource":"opc.tcp://localhost:4840"
    }