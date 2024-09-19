..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)


=========
Beginner
=========

An easy way to start using the SWAP-IT software components is defining a simple PFDL description, which executes a single service.
For the beginner's tutorial, a PFDL is specified that takes a part out of a warehouse. For this to be accomplished, the definition of a set of PFDL structures is required to create the Demonstration Scenario's
SWAP_Order structure. This structure will be reused in the additional tutorials and is handed over from process step to process step, to map the parameter flow of the process.
The Demonstration Scenario's industrial traffic light is composed of stand segments and a number of light segments, so that corresponding PFDL structures are defined to characterize
each element.

.. _Beginner Writing custom PFDL files:

Beginner: Writing custom PFDL files
===================================

For the beginner tutorial, the PFDL structure SWAP_Order is defined that contains all information about the traffic
light to be produced. It is the input and output variable for each process step to enable the parameter flow throughout the process.
The required PFDL is defined as:

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
    #within the OPC UA namespace for the warehouse server
    Struct ResourceAssignment
        job_resource:string
    End

    #production Task the defines the overall process
    Task productionTask
        #define a single service GetPartsFromWarehouse
        GetPartsFromWarehouse
            In
                ResourceAssignment
                {
                    "job_resource":"opc.tcp://host.docker.internal:4840"
                }
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
                          "number_light_segments": 1
                    }

            Out
                order:SWAP_Order
    End

The visual representation of the resulting process is illustrated below:

.. figure:: /images/beginner.PNG
   :alt: Process Visualization Beginner
   :width: 100%

.. _Beginner Building Resource-specific Information Models:

Beginner: Building Resource-specific Information Models
======================================================================

The pre-build Nodeset2.xml files for each namespace in this tutorial can be found within the directory `InformationModels <https://github.com/swap-it/demo-scenario/tree/main/Tutorials/beginner/model>`_.
However, if it is  planned to use the pre-build files, this section can be skipped and the tutorial continues with section `Beginner: Build custom OPC UA Server`_, however the following sections give a short introduction on how to
define *.ModelDesign.xml files, which can then be processed with the `UA-ModelCompiler <https://github.com/OPCFoundation/UA-ModelCompiler>`_ to generate NodeSet2.xml files.

.. toctree::
   :maxdepth: 2

   pfdl_namespace
   warehouse_namespace


Beginner: Build custom OPC UA Server
=====================================

The following sections will provide a step-by-step example on how to start an OPC UA server that can interact with the SWAP-IT software modules.

Beginner: Writing a JSON-configuration file
----------------------------------------------------------

To use the open62541 server template, it is required to provide a JSON-based server configuration. A minimal
version of such a JSON configuration for the warehouse server is shown below:

.. code-block:: c

        {
          application_name: "warehouse",
          resource_ip: "localhost",
          port: "4840",
          module_type: "WarehouseModuleType",
          module_name: "WarehouseModule",
          service_name: "GetPartsFromWarehouse"
        }


Beginner: CMake Configuration
-----------------------------

To load the previously defined OPC UA namespaces into open62541 based OPC UA server, it is required to
configure the CMakeLists.txt file. From this CMake configuration the `open62541 nodeset compiler <https://www.open62541.org/doc/master/nodeset_compiler.html>`_ will generate executable C-code.

An example of such a CMakeLists.txt file is illustrated below:

.. code-block:: c

    cmake_minimum_required(VERSION 3.0.0)
    #set name of the CMakeProject
    project(beginner_tutorial)
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
    #adds the executable target
    add_executable(beginner_tutorial
            beginner.c
            ${UA_NODESET_PFDL_PARAMETER_SOURCES}
            ${UA_TYPES_PFDL_PARAMETER_SOURCES}
            ${UA_NODESET_COMMON_SOURCES}
            ${UA_TYPES_COMMON_SOURCES}
            ${UA_NODESET_WAREHOUSE_SOURCES}
            ${UA_TYPES_WAREHOUSE_SOURCES}
    )
    #add the dependencies between the beginner tutorial and the generated namespaces
    add_dependencies(beginner_tutorial open62541-generator-ns-common
            open62541-generator-ns-pfdl_parameter
            open62541-generator-ns-warehouse
    )
    #link the beginner tutorial project with the installed open62541 server template
    target_link_libraries(beginner_tutorial swap_server_template)
    #link the beginner tutorial project with the installed open62541 SDK
    target_link_libraries(beginner_tutorial open62541::open62541)


Beginner: Build simple server
-----------------------------

To build the first OPC UA server that is compatible with the SWAP-IT software, the open62541 server template is used.
This template builds the internal server structure
that is required by the process agent. In addition, it is required to define a custom method callback for the GetPartsFromWarehouse service:

.. code-block:: c

    #include <stdio.h>
    #include "signal.h"
    #include <open62541/plugin/log_stdout.h>
    #include <open62541/server.h>
    /*include the open62541 server template library*/
    #include "swap_it.h"
    /*include the generated c code from the nodeset compiler*/
    #include "namespace_common_generated.h"
    #include "namespace_pfdl_parameter_generated.h"
    #include "namespace_warehouse_generated.h"
    #include "types_common_generated.h"
    #include "types_pfdl_parameter_generated_handling.h"
    #include "types_common_generated_handling.h"
    #include "warehouse_nodeids.h"

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

Finally, it is possible to write the main function of the server, where the three information models, the common information model, the pfdl parameter model and the warehouse model are loaded. Besides the JSON configuration is provided, so that the
server can be instantiated with the open62541 swap server template's function UA_server_swap_it:

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
        UA_Server *server = UA_Server_new();
        /* load the required namespace with autogenerated functions from the nodeset-compiler*/
        UA_StatusCode retval = namespace_common_generated(server);
        if(retval != UA_STATUSCODE_GOOD) {
            UA_LOG_WARNING(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the common namespace failed. Please check previous error output.");
            UA_Server_delete(server);
        }
        retval = namespace_pfdl_parameter_generated(server);
        if(retval != UA_STATUSCODE_GOOD) {
            UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the pfdl types namespace failed. Please check previous error output.");
            UA_Server_delete(server);
        }
        retval = namespace_warehouse_generated(server);
        if(retval != UA_STATUSCODE_GOOD) {
            UA_LOG_ERROR(UA_Log_Stdout, UA_LOGCATEGORY_SERVER, "Adding the pfdl types namespace failed. Please check previous error output.");
            UA_Server_delete(server);
        }
        /*store the JSON config in a UA_ByteString to hand it over to the UA_server_swap_it function*/
        UA_ByteString conf = UA_String_fromChars("{\n"
                                                 "  application_name: \"warehouse_dr1\",\n"
                                                 "  resource_ip: \"localhost\",\n"
                                                 "  port: \"4840\",\n"
                                                 "  module_type: \"WarehouseModuleType\",\n"
                                                 "  module_name: \"WarehouseModule\",\n"
                                                 "  service_name: \"GetPartsFromWarehouse\",\n"
                                                 "}"); /*loadFile(path_to_config);*/
        /* the structure UA_service_server_interpreter will return the pre-defined information about the server
         * from the json configration*/
        UA_service_server_interpreter swap_server;
        memset(&swap_server, 0, sizeof(UA_service_server_interpreter));
        /* with the function UA_server_swap_it from the open62541 server template,
         * it is possible to configure the OPC UA server with a single function call*/
        UA_server_swap_it(server, conf, warehousemethodCallback, UA_FALSE, &running, UA_FALSE, &swap_server);
        UA_ByteString_clear(&conf);
        /*start and run the server*/
        UA_Server_run_startup(server);
        while(running) {
            UA_Server_run_iterate(server, true);
        }
        //clear memory
        clear_swap_server(&swap_server, UA_FALSE, server);
        UA_LOG_INFO(UA_Log_Stdout, UA_LOGCATEGORY_SERVER,"Shutting down server %s ", swap_server.server_name);
        /*Shut down the server*/
        UA_Server_run_shutdown(server);
        UA_Server_delete(server);
    }

Beginner: Build the Server Executable
-------------------------------------
After the code is completed, it is now possible to compile the server executable. For this to be achieved, create a build directory within the working directory and compile the server with the cmake and make command.

.. code-block:: c

    cd working directory
    mkdir build && cd build
    cmake ..
    make
    /*start the executable*/
    ./beginner_tutorial

In case that the shop floor should be started from the tutorial implementation, execute the following steps:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario.git
    cd demo-scenario/Tutorials/beginner
    mkdir build && cd build
    cmake ..
    make
    /*start the executable*/
    ./beginner_tutorial

Beginner: Execute the PFDL with an Execution Engine
===================================================

Since the code basis of the process agent in not available yet, we provide a docker compose file which starts the missing application, in fact the process agent and the dashboard, in a docker compose project. A corresponding
Docker-compose.yaml file is provided in the `beginner directory <https://github.com/swap-it/demo-scenario/tree/main/Tutorials/beginner>`_.
This docker application can be started with the following steps, so that the beginner tutorial PFDL and the corresponding process is executed on the resource, which is implemented in this tutorial:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario.git
    cd demo-scenario/Tutorials/beginner
    docker-compose up