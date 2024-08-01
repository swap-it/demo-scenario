..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)

How-To-Guides
==============

.. _Start the Patient Zero Docker Application:
Start the Demonstration Scenario Docker Application
------------------------------------------------------------
Pre-build docker containers for all software components that are required to start the Demonstration Scenario docker application are available in the
repository's container registry. With these containers, it is possible to start a docker-compose project to get an initial impression of the Demonstration Scenario application.
To start this docker-compose environment, start docker on your local device. Clone the Demonstration Scenario application repository and enter it from a terminal.
Afterwards, the project can be started with the docker-compose up command:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario
    cd demo-scenario
    docker-compose up

This docker-compose project features the SWAP-IT dashboard. To monitor and start processes from the dashboard, open a browser and connect to

.. code-block:: c

    http://localhost:5000

after the docker-compose project is started.

Build open62541 OPC UA Server
------------------------------

Since our architecture relies heavily on the open62541 open source OPC UA SDK, please have a look at the `open62541 documentation <https://www.open62541.org/doc/master/index.html>`_, in case that
the SDK was never used before. This is a better starting point to for C-base OPC UA server than our architecture.


Writing PFDL files
-------------------

Throughout the Tutorials section, the Demonstration Scenario PFDL is recreated. However, to get a deeper insight into the PFDL and its usage, please have a look at https://iml130.github.io/pfdl/.
