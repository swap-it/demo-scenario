..
    Licensed under the MIT License.
    For details on the licensing terms, see the LICENSE file.
    SPDX-License-Identifier: MIT

    Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)



===================
Gluing Namespace
===================

Next, the gluing information model is defined.

.. code-block:: xml

    <?xml version="1.0" encoding="utf-8" ?>
    <ModelDesign
        xmlns:uax="http://opcfoundation.org/UA/2008/02/Types.xsd"
        xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
        xmlns:ua="http://opcfoundation.org/UA/"
        xmlns:cmn="http://common.swap.fraunhofer.de"
        xmlns:pt="http://swap.demo.scenario.fraunhofer.de"
        xmlns:gl="http://swap.demo.scenario.gluing.fraunhofer.de"
        xmlns:xsd="http://www.w3.org/2001/XMLSchema"
        TargetNamespace="http://swap.demo.scenario.gluing.fraunhofer.de"
        TargetXmlNamespace="http://swap.demo.scenario.gluing.fraunhofer.de"
        TargetVersion="1.00.0"
        TargetPublicationDate="2024-01-01T00:00:00Z"
        xmlns="http://opcfoundation.org/UA/ModelDesign.xsd">

        <Namespaces>
            <Namespace Name="Gluing" Prefix="SWAP.Fraunhofer.Demo.Scenario.Gluing.Model" XmlNamespace="http://swap.demo.scenario.gluing.fraunhofer.de/Types.xsd" XmlPrefix="gl">http://swap.demo.scenario.gluing.fraunhofer.de</Namespace>
            <!-- set dependency to the pfdl types namespace -->
            <Namespace Name="Demo" Prefix="SWAP.Fraunhofer.Demo.Scenario.Model" XmlNamespace="http://swap.demo.scenario.fraunhofer.de/Types.xsd" XmlPrefix="pt" FilePath="DemoScenarioTypes.ModelDesign">http://swap.demo.scenario.fraunhofer.de</Namespace>
            <!-- set dependency to the common namespace -->
            <Namespace Name="Common" Prefix="SWAP.Fraunhofer.Common.Model" XmlNamespace="http://common.swap.fraunhofer.de/Types.xsd" XmlPrefix="cmn" FilePath="CommonModelDesign">http://common.swap.fraunhofer.de</Namespace>
            <Namespace Name="OpcUa" Prefix="Opc.Ua" Version="1.03.00" PublicationDate="2013-12-02T00:00:00Z" InternalPrefix="Opc.Ua.Server" XmlNamespace="http://opcfoundation.org/UA/2008/02/Types.xsd" XmlPrefix="ua">http://opcfoundation.org/UA/</Namespace>
        </Namespaces>


        <ObjectType SymbolicName="gl:Gluing" BaseType="cmn:ServiceFinishedEventType">
            <Children>
                <Property SymbolicName="gl:order" DataType="pt:SWAP_Order" ModellingRule="Mandatory" />
            </Children>
        </ObjectType>

        <ObjectType SymbolicName="gl:ServiceObjectType" BaseType="cmn:ServiceObjectType">
            <Children>
                <Method SymbolicName="cmn:Gluing" TypeDefinition="gl:GluingMethodType"/>
            </Children>
        </ObjectType>

        <ObjectType SymbolicName="gl:GluingModuleType" BaseType="cmn:ModuleType">
            <Children>
                <Object SymbolicName="cmn:Services" TypeDefinition="gl:ServiceObjectType"/>
            </Children>
        </ObjectType>

        <Method SymbolicName="gl:GluingMethodType">
            <InputArguments>
                <Argument Name="order" DataType="pt:SWAP_Order" ModellingRule="Mandatory"/>
            </InputArguments>
            <OutputArguments>
                <Argument Name="Results" DataType="cmn:ServiceExecutionAsyncResultDataType" ModellingRule="Mandatory"/>
            </OutputArguments>
        </Method>
    </ModelDesign>