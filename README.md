

# SWAP-IT Demonstration Scenario

The SWAP-IT architecture (https://ieeexplore.ieee.org/document/9926665) was developed as part of the Fraunhofer lighthouse 
project SWAP-IT (https://www.produktion.fraunhofer.de/en/research/research-cooperations/lighthouse-projects/swap.html). This new
technological concept makes it possible to implement the production of tomorrow by transforming rigid production
environments with individual processing stations into flexible and dynamic production environments. The core of this
innovation is a modular cyber-physical production system (CPPS) that efficiently integrates centralized and
decentralized elements as a network of software and mechanical components. The modules are specially designed
to adapt seamlessly to different production environments and can be further developed with minimal effort.
The Demonstration Scenario is an example process that is executed within the software modules of the SWAP-It
architecture and should server potential users as a first step to get familiar with the SWAP-IT developments and
concepts. In this context, a process to manufacture an industrial traffic light is used as an example. Additional
information about the SWAP-IT architecture, the Demonstration scenario and the SWAP-IT software modules can be
found in the following sections of this documentation.



## SWAP-IT Architecture



The SWAP-IT architecture introduces a modular, scalable and lightweight architecture utilizing simplified semantic
information models. In general, components of the architecture are split into two types of modules. On the one hand,
the Basic Modules are the core components of the architecture by enabling the interaction with the loosely coupled
components. All basic components will be published as open source software projects for a simple adaption to
enable an automate integration of new processes, based on the Production Flow Description Language (PFDL, https://github.com/iml130/pfdl). On
the other hand, Use Case specific Modules integrate existing shop floor assets, either physical assets, such as
production resources, or non-physical assets, such as optimization approaches to assign jobs to resources. Since
these components can vary from production environment to production environment, the SWAP-IT architecture
provides an easy integration mechanism for existing and new assets. As a result, the SWAP-IT architecture enables
industrial production environments with a high degrees of flexibility, easy process integration and in addition,
addresses further challenges of production environments, such as an automated resource parameterization for
individual products, or an easy integration of new assets into a shop floor.

<p align="center">
    <img src="documentation/source/images/swap_it_general.PNG" alt="">
</p>

For this to be achieved, one process module executes exactly one process that is specified in the PFDL format. The
Enterprise Resource Planning (ERP) module is utilized to submit orders and the registry module makes resources
available for process agents. Each resource on a shop floor is defined as a SWAP-IT Asset (Use-Case Specific
Modules). Such assets can easily be defined by utilizing the SWAP-IT Common Information Model
(https://github.com/FraunhoferIOSB/swap-it-common-information-model). As a result, new assets can easily be
integrated into the architecture. An example interaction between the SWAP-IT assets is shown in the sequence
diagram below. As starting point, each shop floor Asset that should be available for the execution of process steps
has to register itself in the Registry Module. Here, an arbitrary number of assets, that are able to execute any given
process step, can register themselves within the Registry Module. A process execution starts with the submission of
an order from a Customer. Afterward, the ERP Module generates the process description (PFDL) for the order and
transmits it to a Process Agent. Within the Process Agent the PFDL Scheduler interprets the PFDL and starts to
schedule specified services. Each scheduled service is then transmitted to the Execution Engine, which in turn,
requests all suitable Assets to execute the service from the Registry Module. Afterward, the Execution Engine
interacts with an Assignment Asset to find the best asset in the context of the specified optimization approach. While
these interaction steps are the recommended way to assign a service to an Asset, the architecture incorporates
additional assignment approaches. Further details about these approaches can be found in section Resource
Assignment. After an Asset is found, the Execution Engine initiates the service execution on this particular asset and
then, returns the service result to the PFDL Scheduler. Although the sequence diagram only displays a single service
execution, the Process Agent can execute an arbitrary number of process steps until the production process of a
product is completed. After the completion of the production process, the ERP Module receives a notification from
the Process Agent and can inform the customer that the order is completed.

<p align="center">
    <img src="documentation/source/images/SWAPSequence.PNG" alt="">
</p>

## Demonstration Scenario

Demonstration Scenario is an initial application of the SWAP-IT architecture that demonstrates the different software
components of the architecture. In this context, the process within the demonstration scenario is constructed to
manufacture an industrial traffic light, with a variable number of light segments. The process is started from the ERP
Module, which spawns a process module and generates a PFDL description of the process. This process is then
executed by a process module, that interacts with the shop floor of the scenario. In this context, the shop floor
consists of a Warehouse Module, a Mounting Module, a Gluing Module a Coating Module and a Milling Module. The
overall process is illustrated in the figure below. Here, the manufacturing of the light segments and the manufacturing
of the stand segment are defined as parallel subprocesses. Both subprocesses start with the taking of parts from the
warehouse. Within the manufacturing of the light segment, a Parallel Loop is deployed within each of the light
segments gets coated in the selected color. After each light segment is coated, they are glued together. Since
the amount of Gluing operations depends on the number of light segments, the Gluing operation is executed within a
Loop condition. As soon as all light segments are glued, this subprocess is completed and the process execution
waits until the stand segment’s subprocess is completed. Within the stand segment’s subprocess a milling operation
is performed on the stand segment to shape it in the desired form. Afterward, this subprocess is completed and the
process execution waits for the compilation of the light segment’s subprocess. When both subprocesses are
completed, the processes are joined together and the light segments are mounted to the stand segment. Further
details about the process and the according PFDL, the demonstration scenario shop floor configuration and the
representation of the resources within the SWAP-IT architecture can be found in section Demonstration Scenario
Application.

<p align="center">
    <img src="documentation/source/images/process_block.PNG" alt="">
</p>

## SWAP-IT Software


The application consists of a set of loosely coupled software components. The figure below depicts all software
components that are currently available as open source projects, or will be published as open source projects within 2024.
The development status and the interaction of these components is shown in the figure below. 

<p align="center">
    <img src="documentation/source/images/img_1.png" alt="">
</p>

An extensive introduction into SWAP-IT and its software modules can be found in the documentation.



## Documentation
To build the documentation, sphinx and the sphinx rtd theme are required. Both can be installed with:

    sphinx sphinx-rtd-theme myst_parser rst2pdf

Build the documentation:

    cd demo-scenario
    #html
    sphinx-build -M html documentation/source/ documentation/build/html
    #pdf
    sphinx-build -b pdf documentation/source/ documentation/build/pdf/


## Docker
Since not all software components required to start the demonstration scenario are available yet, we provide a pre-build docker container
with all components that can be started with the provided docker-compose.yaml file, The application can be started with:

    #clone this repository
    git clone https://github.com/swap-it/demo-scenario
    cd demo-scenario
    docker-compose up

## Requirements

This repository requires a local installation of the open62541 OPC UA SDK (https://github.com/open62541/open62541)
version 1.3.10 and a local installation of the swap it open62541 server template (https://github.com/FraunhoferIOSB/swap-it-open62541-server-template)

At least the following build flags are required for the open62541 OPC UA SDK:

    -DUA_NAMESPACE_ZERO=FULL -DUA_BUILD_SHARED_LIBS=ON

for the open62541 SDK is required.

## Build the Tutorials

    #clone this repository
    cd demo-scenario/Tutorials
    cd beginner
    #cd intermediate
    #cd advanced

    mkdir build && cd build
    cmake ..
    make
    ./beginner_tutorial
    #./intermediate_tutorial
    #./swap_server
