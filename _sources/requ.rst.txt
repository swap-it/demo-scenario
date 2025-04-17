..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)


=============================
Requirements and Installation
=============================

The following software components are required to execute processes with the SWAP-IT architecture. These software modules are also the basis for the tutorial section.
The open62541-server-template is only available for linux OS.

- local installed version of the open62541 OPC UA SDK (https://github.com/open62541/open62541) version 3.10:

.. code-block:: c

    #clone open62541 library and init submodules
    git clone https://github.com/open62541/open62541
    cd open62541
    git fetch --all --tags
    git checkout tags/v1.3.10 -b v1.3.10-branch
    git submodule update --init --recursive

    #install open62541
    mkdir build && cd build
    cmake -DUA_NAMESPACE_ZERO=FULL -DUA_NAMESPACE_ZERO=FULL -DUA_ENABLE_JSON_ENCODING=ON -DBUILD_SHARED_LIBS=ON -DUA_MULTITHREADING=200 ..
    make install

- local installed version of the open62541-server-template (https://github.com/FraunhoferIOSB/swap-it-open62541-server-template):

.. code-block:: c

    git clone https://github.com/FraunhoferIOSB/swap-it-open62541-server-template.git
    cd open62541-server-template
    mkdir build && cd build
    cmake ..
    make install

- Common Information Model:

.. code-block:: c

    git clone https://github.com/FraunhoferIOSB/swap-it-common-information-model

In case it is desired to build the information models yourself, either use the Siome Modelling Editor (https://support.industry.siemens.com/cs/document/109755133/siemens-opc-ua-modeling-editor-(siome)?dti=0&lc=de-DE)
or the UA-ModelCompiler (https://github.com/OPCFoundation/UA-ModelCompiler). The section "Building Resource-specific Information Models" of each tutorial relies on the latter.
However, each tutorial provides a directory that contains all required information models that can also be used directly. Consequently, recreating them is optional.

- Execution Engine:

Either use the pre-build docker container, or clone the Execution Engine repository and start the process agents from there.

- Device Registry

Either use the pre-build docker container, or clone the Device Registry repository and start it from there.

- PFDL Visual Studio Code Extension (https://github.com/iml130/pfdl-vscode-extension):

An optional tool that is useful to debug PFDLs.

- SWAP-IT Dashboard:

Optional tool that maps processes executed by the process agent. For the Demonstration Scenario, it is also used to start processes within the application.