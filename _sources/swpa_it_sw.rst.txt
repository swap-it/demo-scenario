..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)


.. _SWAP-IT Software Modules:
SWAP-IT Software Modules
========================



In the following, a short introduction is given into the SWAP-IT software modules that are used within the Demonstration Scenario application.
The *Order Registry* and the *SWAP-IT Dashboard* are used to mock an Enterprise Resource Planning ERP system. Here, the *Order Registry* allows users to place
an order. For this order, a new *Process Agent* is spawned, who then executes the specified sequence of process steps.
The *SWAP-IT Dashboard* is connected to the *Production Flow Description Language - Scheduler (PFDL-Scheduler)* and displays the visual representation during its execution,
as well as additional information such as the start and the finish of services or tasks.
A *Process Agent* consist of an
*Execution Engine* and a *PFDL-Scheduler*. *Process Agents* are created for each individual PFDL (Production Flow Description Language) execution. The field level resources
represent physical resources in an industrial production environment. These resources have to be mapped to an OPC UA server to become compatible with the *SWAP-IT Software Components*.
For such servers,
the *Common Information Model* is required. In case of an implementation with the open62541 OPC UA SDK, it is highly
recommended to use the `swap-it-open62541-server-template <https://github.com/FraunhoferIOSB/swap-it-open62541-server-template>`_. Additional information about each software component can be found in the following sections.

Process Agent
--------------


The heart of the SWAP-IT software components is the process agent, consisting of a PFDL-Scheduler and an Execution Engine. Each process agent receives a process description in a PFDL format. The PFDL is a domain specific language, from which the PFDL-Scheduler generates and executes a petri net. Here, the Execution Engine serves as an
interface between the
scheduler and the Field-Level-Devices. Their communication is accomplished with OPC UA. Since the PFDL description allows to define services to be executed on the shop floor, the Execution Engine requires a functionality to identify resources, which are able to
execute the scheduled service. For more information about the Execution Engine's resource assignment have a look at section `Resource Assignment`_. In addition, the Execution Engine needs a generic way to execute services.
For this, a standardized information model is provided, which has to be included into all servers. This information model is called `Common Information Model`_ and defines how assets
must provide information about themselves, such as callable services, states and capabilities.


.. _Resource Assignment:

Resource Assignment
^^^^^^^^^^^^^^^^^^^^^^^^^^^^

The process agent has two possibilities to assign services to resources. First, the resource can either be specified statically
in the PFDL description by defining a ResourceAssignment structure and providing it as service input.

.. code-block:: c

    ResourceAssignment
    {
        "job_resource":"opc.tcp://0.0.0.0:4840"
    }

Second, the process agent dynamically searches for a resource in the Device Registry. For this to be accomplished, a resource has to register itself in the Device Registry and thus, becomes visible to the system. Next, the Process Agent's Execution Engine
filters a list of possible resources. This list is then transmitted to an Assignment Agent, who assigns the job to one particular resource from this list. The default behavior of the assigment agent is to assign the job to the resource with the fewest
elements in its queue, independent of any processing times or costs. The default behavior ensures that each process is completely executed, however to integrate
more complex assignment strategies, custom Assignment Agents can be defined and integrated into applications. While the Assignment Agent is currently defined as an individual entity, the default Assignment Agent will be included into the Execution Engine. Consequently,
there will be no more need for such an agent, in case that the default behavior is desired. However, for custom assignment behavior, custom assignment agents can be integrated.

.. figure:: /images/interaction.PNG
   :alt: Resource Assignment with assignment agent
   :width: 100%


Registry Module
----------------------------

The Registry Module is used to make resources, represented by the resource's server, available to the system. In this context, assignment agents search the device registry for resource that are able to execute particular services.
Resources are only visible to the assignment agents and thus, make themself available to execute PFDL Services, after they registered themself in a device registry.
In addition, the Device Registry has a build-in functionality to filter suitable resources for a service execution based on capabilities of a resource.
A scenario for such a filtering is for instance the assignment of a transport task to an autonomous-guided-vehicle (AGV) where each AGV can carry a maximum payload.
Now the Execution Engine can call the Device Registry's Filter Agent Method to
receive a list of all AGVs that are able to carry a concrete product. Here, the Device Registry considers the product's weight and the resources maximum payload
to filter suitable resources.

Common Information Model
----------------------------

The `Common Information Model <https://common-information-model-swap-entwicklungen-swap-5c35379a702364.pages.fraunhofer.de/>`_ needs to be included into an OPC UA Server so that an Process Agent can execute a specified service.

.. figure:: /images/moduletype.PNG
   :alt: Module Type in the common information model
   :width: 100%

Order Registry
--------------

The Order Registry is a module that can be compared to an ERP (Enterprise Resource Planning) system, since it enables the start of new production processes by spawning process agents.

Assignment Agent
-----------------

Class of agents that interact with process agents and registry modules to assign services to resources. in this context, an assignment agent is triggered by a process agent to find a resource for a service to be executed. Afterwards the assignment agent connects to
the device registry to filter a list of suitable agents. From this list, the agent assigns the service to one particular resource, based on a custom assignment strategy. Afterwards, it returns the selected resource to the process agent,
who then initiates the service execution on the returned resource.

open62541 Server Template
----------------------------

The `open62541 swap server template <https://github.com/FraunhoferIOSB/swap-it-open62541-server-template>`_ simplifies the process of making a open62541 OPC UA server compatible with the software modules, so that the server can provide a service that can be invoked by a process agent. The template provides a single function UA_Server_swap_it to instantiate
a module specific Subtype of the Module Type. Besides, it provides functionalities for Field-Level-Devices
to register and unregister themselves in a Registry Module, and a queue handler that provides the functionality to add and remove elements from a queue.

SWAP-IT Dashboard
----------------------------

The SWAP-IT dashboard is a tool that was initially created to monitor processes that are driven by the PFDL-Scheduler. However, its basic functionality will be extended so that new process agents can be spawned within the
Demonstration Scenario environment.

.. figure:: /images/dashboard.PNG
   :alt: SWAP-IT Dashboard
   :width: 100%