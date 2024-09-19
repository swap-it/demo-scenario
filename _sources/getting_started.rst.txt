..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)



Getting Started
================

The next sections should provide a short introduction into the SWAP-IT software components by demonstrating its usage in a set of tutorials for beginners, intermediate- and
advanced users. The Demonstration Scenario is rebuilt step by step over the different difficulty levels.
The code of each tutorial is provided within the Tutorials directory of the Git Repository (https://github.com/swap-it/demo-scenario).
Please ensure to meet the requirements specified in section :ref:`Requirements and Installation`.
The corresponding executable of each tutorial can be build and started with:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario.git
    cd demo-scenario/Tutorials/beginner
    #cd demo-scenario/Tutorials/intermediate
    #cd demo-scenario/Tutorials/advanced
    mkdir build && cd build
    cmake ..
    make
    ./beginner_tutorial
    #./intermediate_tutorial
    #./swap_server

Besides the tutorials, a set of docker containers whereat each of them contains a software implementation of a SWAP-IT module can be executed for an initial impression of the functionality. All containers are linked together in a docker compose project
that can be started with:

.. code-block:: c

    git clone https://github.com/swap-it/demo-scenario
    cd demo-scenario
    docker-composed up

The executed process is mounted into the docker-container of the execution-engine and the default process is the process of the advanced tutorial. Besides, the process can be customized
by either using the intermediate.pfdl or the beginner.pfdl files. To change the process, adjust line 55 of the docker-compose.yaml file, located in the demo-scenario directory:

.. code-block:: c

    54     volumes:
    55      - ./pfdl/advanced.pfdl:/execution-engine/PFDL_Examples/temp.pfdl
    #replace ./pfdl/advanced.pfdl with a path to a custom pfdl description
    54     volumes:
    55      - path-to-custom-pfdl:/execution-engine/PFDL_Examples/temp.pfdl


While the kind and the number of the shop floor resources is fixed, customized processes can included by defining custom .pfdl files and mounting them into the project
by setting the path to the file, as described above. However, it has to be considered, that each Service call requires a SWAP_Order parameter as input argument.


.. toctree::
   :maxdepth: 2

   requ
   tutorial