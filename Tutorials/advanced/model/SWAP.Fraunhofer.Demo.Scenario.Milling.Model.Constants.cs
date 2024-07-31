/* ========================================================================
 * Copyright (c) 2005-2019 The OPC Foundation, Inc. All rights reserved.
 *
 * OPC Foundation MIT License 1.00
 *
 * Permission is hereby granted, free of charge, to any person
 * obtaining a copy of this software and associated documentation
 * files (the "Software"), to deal in the Software without
 * restriction, including without limitation the rights to use,
 * copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the
 * Software is furnished to do so, subject to the following
 * conditions:
 *
 * The above copyright notice and this permission notice shall be
 * included in all copies or substantial portions of the Software.
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
 * EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
 * OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
 * NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
 * HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
 * WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
 * OTHER DEALINGS IN THE SOFTWARE.
 *
 * The complete license agreement can be found here:
 * http://opcfoundation.org/License/MIT/1.00/
 * ======================================================================*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Xml;
using System.Runtime.Serialization;
using SWAP.Fraunhofer.Demo.Scenario.Model;
using SWAP.Fraunhofer.Common.Model;
using Opc.Ua;

namespace SWAP.Fraunhofer.Demo.Scenario.Milling.Model
{
    #region Method Identifiers
    /// <summary>
    /// A class that declares constants for all Methods in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Methods
    {
        /// <summary>
        /// The identifier for the ServiceObjectType_Milling Method.
        /// </summary>
        public const uint ServiceObjectType_Milling = 15021;

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_add_queue_element Method.
        /// </summary>
        public const uint MillingModuleType_Queue_ServiceQueue_add_queue_element = 15039;

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_remove_queue_element Method.
        /// </summary>
        public const uint MillingModuleType_Queue_ServiceQueue_remove_queue_element = 15041;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_register Method.
        /// </summary>
        public const uint MillingModuleType_Services_register = 15046;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_unregister Method.
        /// </summary>
        public const uint MillingModuleType_Services_unregister = 15048;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_Milling Method.
        /// </summary>
        public const uint MillingModuleType_Services_Milling = 15054;
    }
    #endregion

    #region Object Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Objects
    {
        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Asset Object.
        /// </summary>
        public const uint MillingModuleType_Properties_Asset = 15027;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass Object.
        /// </summary>
        public const uint MillingModuleType_Properties_AssetClass = 15030;

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue Object.
        /// </summary>
        public const uint MillingModuleType_Queue_ServiceQueue = 15037;

        /// <summary>
        /// The identifier for the MillingModuleType_Services Object.
        /// </summary>
        public const uint MillingModuleType_Services = 15045;
    }
    #endregion

    #region ObjectType Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypes
    {
        /// <summary>
        /// The identifier for the Milling ObjectType.
        /// </summary>
        public const uint Milling = 15001;

        /// <summary>
        /// The identifier for the ServiceObjectType ObjectType.
        /// </summary>
        public const uint ServiceObjectType = 15015;

        /// <summary>
        /// The identifier for the MillingModuleType ObjectType.
        /// </summary>
        public const uint MillingModuleType = 15024;
    }
    #endregion

    #region Variable Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Variables
    {
        /// <summary>
        /// The identifier for the Milling_order Variable.
        /// </summary>
        public const uint Milling_order = 15014;

        /// <summary>
        /// The identifier for the ServiceObjectType_register_InputArguments Variable.
        /// </summary>
        public const uint ServiceObjectType_register_InputArguments = 15017;

        /// <summary>
        /// The identifier for the ServiceObjectType_unregister_InputArguments Variable.
        /// </summary>
        public const uint ServiceObjectType_unregister_InputArguments = 15019;

        /// <summary>
        /// The identifier for the ServiceObjectType_Milling_InputArguments Variable.
        /// </summary>
        public const uint ServiceObjectType_Milling_InputArguments = 15022;

        /// <summary>
        /// The identifier for the ServiceObjectType_Milling_OutputArguments Variable.
        /// </summary>
        public const uint ServiceObjectType_Milling_OutputArguments = 15023;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Asset_SerialNumber Variable.
        /// </summary>
        public const uint MillingModuleType_Properties_Asset_SerialNumber = 15028;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Asset_Vendor Variable.
        /// </summary>
        public const uint MillingModuleType_Properties_Asset_Vendor = 15029;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_CommonModelVersion Variable.
        /// </summary>
        public const uint MillingModuleType_Properties_AssetClass_CommonModelVersion = 15031;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_ModuleOrganization Variable.
        /// </summary>
        public const uint MillingModuleType_Properties_AssetClass_ModuleOrganization = 15032;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_ModuleProfileName Variable.
        /// </summary>
        public const uint MillingModuleType_Properties_AssetClass_ModuleProfileName = 15033;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_ModuleProfileVersion Variable.
        /// </summary>
        public const uint MillingModuleType_Properties_AssetClass_ModuleProfileVersion = 15034;

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Stationary Variable.
        /// </summary>
        public const uint MillingModuleType_Properties_Stationary = 15035;

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_queue_variable Variable.
        /// </summary>
        public const uint MillingModuleType_Queue_ServiceQueue_queue_variable = 15038;

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_add_queue_element_InputArguments Variable.
        /// </summary>
        public const uint MillingModuleType_Queue_ServiceQueue_add_queue_element_InputArguments = 15040;

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_remove_queue_element_InputArguments Variable.
        /// </summary>
        public const uint MillingModuleType_Queue_ServiceQueue_remove_queue_element_InputArguments = 15042;

        /// <summary>
        /// The identifier for the MillingModuleType_Registry_Subscriptions_Subscription_Objects Variable.
        /// </summary>
        public const uint MillingModuleType_Registry_Subscriptions_Subscription_Objects = 15044;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_register_InputArguments Variable.
        /// </summary>
        public const uint MillingModuleType_Services_register_InputArguments = 15047;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_unregister_InputArguments Variable.
        /// </summary>
        public const uint MillingModuleType_Services_unregister_InputArguments = 15049;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_registered Variable.
        /// </summary>
        public const uint MillingModuleType_Services_registered = 15050;

        /// <summary>
        /// The identifier for the MillingModuleType_State_AssetState Variable.
        /// </summary>
        public const uint MillingModuleType_State_AssetState = 15052;

        /// <summary>
        /// The identifier for the MillingModuleType_State_Location Variable.
        /// </summary>
        public const uint MillingModuleType_State_Location = 15053;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_Milling_InputArguments Variable.
        /// </summary>
        public const uint MillingModuleType_Services_Milling_InputArguments = 15055;

        /// <summary>
        /// The identifier for the MillingModuleType_Services_Milling_OutputArguments Variable.
        /// </summary>
        public const uint MillingModuleType_Services_Milling_OutputArguments = 15056;
    }
    #endregion

    #region Method Node Identifiers
    /// <summary>
    /// A class that declares constants for all Methods in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class MethodIds
    {
        /// <summary>
        /// The identifier for the ServiceObjectType_Milling Method.
        /// </summary>
        public static readonly ExpandedNodeId ServiceObjectType_Milling = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Methods.ServiceObjectType_Milling, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_add_queue_element Method.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Queue_ServiceQueue_add_queue_element = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Methods.MillingModuleType_Queue_ServiceQueue_add_queue_element, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_remove_queue_element Method.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Queue_ServiceQueue_remove_queue_element = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Methods.MillingModuleType_Queue_ServiceQueue_remove_queue_element, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_register Method.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_register = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Methods.MillingModuleType_Services_register, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_unregister Method.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_unregister = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Methods.MillingModuleType_Services_unregister, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_Milling Method.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_Milling = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Methods.MillingModuleType_Services_Milling, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);
    }
    #endregion

    #region Object Node Identifiers
    /// <summary>
    /// A class that declares constants for all Objects in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectIds
    {
        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Asset Object.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_Asset = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Objects.MillingModuleType_Properties_Asset, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass Object.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_AssetClass = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Objects.MillingModuleType_Properties_AssetClass, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue Object.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Queue_ServiceQueue = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Objects.MillingModuleType_Queue_ServiceQueue, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services Object.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Objects.MillingModuleType_Services, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);
    }
    #endregion

    #region ObjectType Node Identifiers
    /// <summary>
    /// A class that declares constants for all ObjectTypes in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class ObjectTypeIds
    {
        /// <summary>
        /// The identifier for the Milling ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId Milling = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.ObjectTypes.Milling, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the ServiceObjectType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId ServiceObjectType = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.ObjectTypes.ServiceObjectType, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType ObjectType.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.ObjectTypes.MillingModuleType, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);
    }
    #endregion

    #region Variable Node Identifiers
    /// <summary>
    /// A class that declares constants for all Variables in the Model Design.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class VariableIds
    {
        /// <summary>
        /// The identifier for the Milling_order Variable.
        /// </summary>
        public static readonly ExpandedNodeId Milling_order = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.Milling_order, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the ServiceObjectType_register_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ServiceObjectType_register_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.ServiceObjectType_register_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the ServiceObjectType_unregister_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ServiceObjectType_unregister_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.ServiceObjectType_unregister_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the ServiceObjectType_Milling_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ServiceObjectType_Milling_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.ServiceObjectType_Milling_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the ServiceObjectType_Milling_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId ServiceObjectType_Milling_OutputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.ServiceObjectType_Milling_OutputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Asset_SerialNumber Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_Asset_SerialNumber = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Properties_Asset_SerialNumber, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Asset_Vendor Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_Asset_Vendor = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Properties_Asset_Vendor, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_CommonModelVersion Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_AssetClass_CommonModelVersion = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Properties_AssetClass_CommonModelVersion, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_ModuleOrganization Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_AssetClass_ModuleOrganization = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Properties_AssetClass_ModuleOrganization, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_ModuleProfileName Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_AssetClass_ModuleProfileName = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Properties_AssetClass_ModuleProfileName, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_AssetClass_ModuleProfileVersion Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_AssetClass_ModuleProfileVersion = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Properties_AssetClass_ModuleProfileVersion, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Properties_Stationary Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Properties_Stationary = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Properties_Stationary, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_queue_variable Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Queue_ServiceQueue_queue_variable = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Queue_ServiceQueue_queue_variable, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_add_queue_element_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Queue_ServiceQueue_add_queue_element_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Queue_ServiceQueue_add_queue_element_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Queue_ServiceQueue_remove_queue_element_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Queue_ServiceQueue_remove_queue_element_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Queue_ServiceQueue_remove_queue_element_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Registry_Subscriptions_Subscription_Objects Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Registry_Subscriptions_Subscription_Objects = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Registry_Subscriptions_Subscription_Objects, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_register_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_register_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Services_register_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_unregister_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_unregister_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Services_unregister_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_registered Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_registered = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Services_registered, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_State_AssetState Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_State_AssetState = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_State_AssetState, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_State_Location Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_State_Location = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_State_Location, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_Milling_InputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_Milling_InputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Services_Milling_InputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);

        /// <summary>
        /// The identifier for the MillingModuleType_Services_Milling_OutputArguments Variable.
        /// </summary>
        public static readonly ExpandedNodeId MillingModuleType_Services_Milling_OutputArguments = new ExpandedNodeId(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Variables.MillingModuleType_Services_Milling_OutputArguments, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling);
    }
    #endregion

    #region BrowseName Declarations
    /// <summary>
    /// Declares all of the BrowseNames used in the Model Design.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class BrowseNames
    {
        /// <summary>
        /// The BrowseName for the Milling component.
        /// </summary>
        public const string Milling = "Milling";

        /// <summary>
        /// The BrowseName for the MillingModuleType component.
        /// </summary>
        public const string MillingModuleType = "MillingModuleType";

        /// <summary>
        /// The BrowseName for the order component.
        /// </summary>
        public const string order = "order";

        /// <summary>
        /// The BrowseName for the ServiceObjectType component.
        /// </summary>
        public const string ServiceObjectType = "ServiceObjectType";
    }
    #endregion

    #region Namespace Declarations
    /// <summary>
    /// Defines constants for all namespaces referenced by the model design.
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public static partial class Namespaces
    {
        /// <summary>
        /// The URI for the Milling namespace (.NET code namespace is 'SWAP.Fraunhofer.Demo.Scenario.Milling.Model').
        /// </summary>
        public const string Milling = "http://swap.demo.scenario.milling.fraunhofer.de";

        /// <summary>
        /// The URI for the MillingXsd namespace (.NET code namespace is 'SWAP.Fraunhofer.Demo.Scenario.Milling.Model').
        /// </summary>
        public const string MillingXsd = "http://swap.demo.scenario.milling.fraunhofer.de/Types.xsd";

        /// <summary>
        /// The URI for the Demo namespace (.NET code namespace is 'SWAP.Fraunhofer.Demo.Scenario.Model').
        /// </summary>
        public const string Demo = "http://swap.demo.scenario.fraunhofer.de";

        /// <summary>
        /// The URI for the DemoXsd namespace (.NET code namespace is 'SWAP.Fraunhofer.Demo.Scenario.Model').
        /// </summary>
        public const string DemoXsd = "http://swap.demo.scenario.fraunhofer.de/Types.xsd";

        /// <summary>
        /// The URI for the Common namespace (.NET code namespace is 'SWAP.Fraunhofer.Common.Model').
        /// </summary>
        public const string Common = "http://common.swap.fraunhofer.de";

        /// <summary>
        /// The URI for the CommonXsd namespace (.NET code namespace is 'SWAP.Fraunhofer.Common.Model').
        /// </summary>
        public const string CommonXsd = "http://common.swap.fraunhofer.de/Types.xsd";

        /// <summary>
        /// The URI for the OpcUa namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUa = "http://opcfoundation.org/UA/";

        /// <summary>
        /// The URI for the OpcUaXsd namespace (.NET code namespace is 'Opc.Ua').
        /// </summary>
        public const string OpcUaXsd = "http://opcfoundation.org/UA/2008/02/Types.xsd";
    }
    #endregion
}