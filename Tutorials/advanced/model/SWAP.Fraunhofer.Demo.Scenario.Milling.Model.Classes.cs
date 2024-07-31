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
using System.Xml;
using System.Runtime.Serialization;
using SWAP.Fraunhofer.Demo.Scenario.Model;
using SWAP.Fraunhofer.Common.Model;
using Opc.Ua;

namespace SWAP.Fraunhofer.Demo.Scenario.Milling.Model
{
    #region MillingState Class
    #if (!OPCUA_EXCLUDE_MillingState)
    /// <summary>
    /// Stores an instance of the Milling ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class MillingState : ServiceFinishedEventState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public MillingState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.ObjectTypes.Milling, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the instance with a node.
        /// </summary>
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAAC8AAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1pbGxpbmcuZnJhdW5ob2Zlci5kZScAAABo" +
           "dHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLmZyYXVuaG9mZXIuZGUgAAAAaHR0cDovL2NvbW1vbi5zd2Fw" +
           "LmZyYXVuaG9mZXIuZGX/////BGCAAgEAAAABAA8AAABNaWxsaW5nSW5zdGFuY2UBAZk6AQGZOpk6AAD/" +
           "////DAAAABVgiQoCAAAAAAAHAAAARXZlbnRJZAEBmjoALgBEmjoAAAAP/////wEB/////wAAAAAVYIkK" +
           "AgAAAAAACQAAAEV2ZW50VHlwZQEBmzoALgBEmzoAAAAR/////wEB/////wAAAAAVYIkKAgAAAAAACgAA" +
           "AFNvdXJjZU5vZGUBAZw6AC4ARJw6AAAAEf////8BAf////8AAAAAFWCJCgIAAAAAAAoAAABTb3VyY2VO" +
           "YW1lAQGdOgAuAESdOgAAAAz/////AQH/////AAAAABVgiQoCAAAAAAAEAAAAVGltZQEBnjoALgBEnjoA" +
           "AAEAJgH/////AQH/////AAAAABVgiQoCAAAAAAALAAAAUmVjZWl2ZVRpbWUBAZ86AC4ARJ86AAABACYB" +
           "/////wEB/////wAAAAAVYIkKAgAAAAAABwAAAE1lc3NhZ2UBAaE6AC4ARKE6AAAAFf////8BAf////8A" +
           "AAAAFWCJCgIAAAAAAAgAAABTZXZlcml0eQEBojoALgBEojoAAAAF/////wEB/////wAAAAAVYIkKAgAA" +
           "AAMAFgAAAFNlcnZpY2VFeGVjdXRpb25SZXN1bHQBAaM6AC4ARKM6AAABA8I6/////wEB/////wAAAAAV" +
           "YIkKAgAAAAMADAAAAHNlcnZpY2VfdXVpZAEBpDoALgBEpDoAAAAM/////wEB/////wAAAAAVYIkKAgAA" +
           "AAMACQAAAHRhc2tfdXVpZAEBpToALgBEpToAAAAM/////wEB/////wAAAAAVYIkKAgAAAAEABQAAAG9y" +
           "ZGVyAQGmOgAuAESmOgAAAQKdOv////8BAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public PropertyState<SWAP_Order> order
        {
            get
            {
                return m_order;
            }

            set
            {
                if (!Object.ReferenceEquals(m_order, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_order = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_order != null)
            {
                children.Add(m_order);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case SWAP.Fraunhofer.Demo.Scenario.Milling.Model.BrowseNames.order:
                {
                    if (createOrReplace)
                    {
                        if (order == null)
                        {
                            if (replacement == null)
                            {
                                order = new PropertyState<SWAP_Order>(this);
                            }
                            else
                            {
                                order = (PropertyState<SWAP_Order>)replacement;
                            }
                        }
                    }

                    instance = order;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private PropertyState<SWAP_Order> m_order;
        #endregion
    }
    #endif
    #endregion

    #region ServiceObjectState Class
    #if (!OPCUA_EXCLUDE_ServiceObjectState)
    /// <summary>
    /// Stores an instance of the ServiceObjectType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class ServiceObjectState : ServiceObjectState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public ServiceObjectState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.ObjectTypes.ServiceObjectType, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the instance with a node.
        /// </summary>
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAAC8AAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1pbGxpbmcuZnJhdW5ob2Zlci5kZScAAABo" +
           "dHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLmZyYXVuaG9mZXIuZGUgAAAAaHR0cDovL2NvbW1vbi5zd2Fw" +
           "LmZyYXVuaG9mZXIuZGX/////BGCAAgEAAAABABkAAABTZXJ2aWNlT2JqZWN0VHlwZUluc3RhbmNlAQGn" +
           "OgEBpzqnOgAA/////wQAAAAEYYIKBAAAAAMACAAAAHJlZ2lzdGVyAQGoOgAvAQPiOqg6AAABAf////8B" +
           "AAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBqToALgBEqToAAJYCAAAAAQAqAQEbAAAADAAA" +
           "AFJlZ2lzdHJ5IFVSTAAM/////wAAAAAAAQAqAQEbAAAADAAAAFJlc291cmNlIFVSTAAM/////wAAAAAA" +
           "AQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAEYYIKBAAAAAMACgAAAHVucmVnaXN0ZXIBAao6AC8BA+Q6" +
           "qjoAAAEB/////wEAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQGrOgAuAESrOgAAlgIAAAAB" +
           "ACoBARsAAAAMAAAAUmVnaXN0cnkgVVJMAAz/////AAAAAAABACoBARsAAAAMAAAAUmVzb3VyY2UgVVJM" +
           "AAz/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAABVgiQoCAAAAAwAKAAAAcmVnaXN0ZXJl" +
           "ZAEBrDoALwA/rDoAAAAB/////wMD/////wAAAAAEYYIKBAAAAAMABwAAAE1pbGxpbmcBAa06AC8BAa06" +
           "rToAAAEB/////wIAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQGuOgAuAESuOgAAlgEAAAAB" +
           "ACoBARYAAAAFAAAAb3JkZXIBAp06/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAXYKkK" +
           "AgAAAAAADwAAAE91dHB1dEFyZ3VtZW50cwEBrzoALgBErzoAAJYBAAAAAQAqAQEYAAAABwAAAFJlc3Vs" +
           "dHMBA8w6/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public MillingMethodState Milling
        {
            get
            {
                return m_millingMethod;
            }

            set
            {
                if (!Object.ReferenceEquals(m_millingMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_millingMethod = value;
            }
        }
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Populates a list with the children that belong to the node.
        /// </summary>
        /// <param name="context">The context for the system being accessed.</param>
        /// <param name="children">The list of children to populate.</param>
        public override void GetChildren(
            ISystemContext context,
            IList<BaseInstanceState> children)
        {
            if (m_millingMethod != null)
            {
                children.Add(m_millingMethod);
            }

            base.GetChildren(context, children);
        }

        /// <summary>
        /// Finds the child with the specified browse name.
        /// </summary>
        protected override BaseInstanceState FindChild(
            ISystemContext context,
            QualifiedName browseName,
            bool createOrReplace,
            BaseInstanceState replacement)
        {
            if (QualifiedName.IsNull(browseName))
            {
                return null;
            }

            BaseInstanceState instance = null;

            switch (browseName.Name)
            {
                case SWAP.Fraunhofer.Common.Model.BrowseNames.Milling:
                {
                    if (createOrReplace)
                    {
                        if (Milling == null)
                        {
                            if (replacement == null)
                            {
                                Milling = new MillingMethodState(this);
                            }
                            else
                            {
                                Milling = (MillingMethodState)replacement;
                            }
                        }
                    }

                    instance = Milling;
                    break;
                }
            }

            if (instance != null)
            {
                return instance;
            }

            return base.FindChild(context, browseName, createOrReplace, replacement);
        }
        #endregion

        #region Private Fields
        private MillingMethodState m_millingMethod;
        #endregion
    }
    #endif
    #endregion

    #region MillingModuleState Class
    #if (!OPCUA_EXCLUDE_MillingModuleState)
    /// <summary>
    /// Stores an instance of the MillingModuleType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class MillingModuleState : ModuleState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public MillingModuleState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(SWAP.Fraunhofer.Demo.Scenario.Milling.Model.ObjectTypes.MillingModuleType, SWAP.Fraunhofer.Demo.Scenario.Milling.Model.Namespaces.Milling, namespaceUris);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the instance with a node.
        /// </summary>
        protected override void Initialize(ISystemContext context, NodeState source)
        {
            InitializeOptionalChildren(context);
            base.Initialize(context, source);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAAC8AAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1pbGxpbmcuZnJhdW5ob2Zlci5kZScAAABo" +
           "dHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLmZyYXVuaG9mZXIuZGUgAAAAaHR0cDovL2NvbW1vbi5zd2Fw" +
           "LmZyYXVuaG9mZXIuZGX/////BGCAAgEAAAABABkAAABNaWxsaW5nTW9kdWxlVHlwZUluc3RhbmNlAQGw" +
           "OgEBsDqwOgAA/////wYAAAAEYIAKAQAAAAMADAAAAENhcGFiaWxpdGllcwEBsToALwA6sToAAP////8A" +
           "AAAABGCACgEAAAADAAoAAABQcm9wZXJ0aWVzAQGyOgAvADqyOgAA/////wMAAAAEYIAKAQAAAAMABQAA" +
           "AEFzc2V0AQGzOgAvADqzOgAA/////wIAAAAVYIkKAgAAAAMADAAAAFNlcmlhbE51bWJlcgEBtDoALwA/" +
           "tDoAAAAM/////wMD/////wAAAAAVYIkKAgAAAAMABgAAAFZlbmRvcgEBtToALwA/tToAAAAM/////wMD" +
           "/////wAAAAAEYIAKAQAAAAMACgAAAEFzc2V0Q2xhc3MBAbY6AC8AOrY6AAD/////BAAAABVgiQoCAAAA" +
           "AwASAAAAQ29tbW9uTW9kZWxWZXJzaW9uAQG3OgAvAD+3OgAAAAz/////AwP/////AAAAABVgiQoCAAAA" +
           "AwASAAAATW9kdWxlT3JnYW5pemF0aW9uAQG4OgAvAD+4OgAAAQO6Ov////8DA/////8AAAAAFWCJCgIA" +
           "AAADABEAAABNb2R1bGVQcm9maWxlTmFtZQEBuToALwA/uToAAAAM/////wMD/////wAAAAAVYIkKAgAA" +
           "AAMAFAAAAE1vZHVsZVByb2ZpbGVWZXJzaW9uAQG6OgAvAD+6OgAAAAT/////AwP/////AAAAABVgiQoC" +
           "AAAAAwAKAAAAU3RhdGlvbmFyeQEBuzoALwA/uzoAAAAB/////wMD/////wAAAAAEYIAKAQAAAAMABQAA" +
           "AFF1ZXVlAQG8OgAvADq8OgAA/////wEAAAAEYIAKAQAAAAMADAAAAFNlcnZpY2VRdWV1ZQEBvToALwED" +
           "2zq9OgAA/////wMAAAAVYIkKAgAAAAMADgAAAHF1ZXVlX3ZhcmlhYmxlAQG+OgAvAD++OgAAAQPLOv//" +
           "//8DA/////8AAAAABGGCCgQAAAADABEAAABhZGRfcXVldWVfZWxlbWVudAEBvzoALwED3Tq/OgAAAQH/" +
           "////AQAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAcA6AC4ARMA6AACWAQAAAAEAKgEBGgAA" +
           "AAkAAABuZXcgaW5wdXQBA8s6/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAEYYIKBAAA" +
           "AAMAFAAAAHJlbW92ZV9xdWV1ZV9lbGVtZW50AQHBOgAvAQPfOsE6AAABAf////8BAAAAF2CpCgIAAAAA" +
           "AA4AAABJbnB1dEFyZ3VtZW50cwEBwjoALgBEwjoAAJYBAAAAAQAqAQEfAAAADgAAAHJlbW92ZV9lbGVt" +
           "ZW50AQPLOv////8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf////8AAAAABGCACgEAAAADABYAAABSZWdp" +
           "c3RyeV9TdWJzY3JpcHRpb25zAQHDOgAvADrDOgAA/////wEAAAAVYIkKAgAAAAMAFAAAAFN1YnNjcmlw" +
           "dGlvbl9PYmplY3RzAQHEOgAvAD/EOgAAABT/////AwP/////AAAAAARggAoBAAAAAwAIAAAAU2Vydmlj" +
           "ZXMBAcU6AC8BAac6xToAAP////8EAAAABGGCCgQAAAADAAgAAAByZWdpc3RlcgEBxjoALwED4jrGOgAA" +
           "AQH/////AQAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAcc6AC4ARMc6AACWAgAAAAEAKgEB" +
           "GwAAAAwAAABSZWdpc3RyeSBVUkwADP////8AAAAAAAEAKgEBGwAAAAwAAABSZXNvdXJjZSBVUkwADP//" +
           "//8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf////8AAAAABGGCCgQAAAADAAoAAAB1bnJlZ2lzdGVyAQHI" +
           "OgAvAQPkOsg6AAABAf////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEByToALgBEyToA" +
           "AJYCAAAAAQAqAQEbAAAADAAAAFJlZ2lzdHJ5IFVSTAAM/////wAAAAAAAQAqAQEbAAAADAAAAFJlc291" +
           "cmNlIFVSTAAM/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAVYIkKAgAAAAMACgAAAHJl" +
           "Z2lzdGVyZWQBAco6AC8AP8o6AAAAAf////8DA/////8AAAAABGGCCgQAAAADAAcAAABNaWxsaW5nAQHO" +
           "OgAvAQGtOs46AAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBzzoALgBEzzoA" +
           "AJYBAAAAAQAqAQEWAAAABQAAAG9yZGVyAQKdOv////8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf////8A" +
           "AAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAdA6AC4ARNA6AACWAQAAAAEAKgEBGAAAAAcA" +
           "AABSZXN1bHRzAQPMOv////8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf////8AAAAABGCACgEAAAADAAUA" +
           "AABTdGF0ZQEByzoALwA6yzoAAP////8CAAAAFWCJCgIAAAADAAoAAABBc3NldFN0YXRlAQHMOgAvAD/M" +
           "OgAAAQO8Ov////8DA/////8AAAAAFWCJCgIAAAADAAgAAABMb2NhdGlvbgEBzToALwA/zToAAAAM////" +
           "/wMD/////wAAAAA=";
        #endregion
        #endif
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        #endregion

        #region Private Fields
        #endregion
    }
    #endif
    #endregion

    #region MillingMethodState Class
    #if (!OPCUA_EXCLUDE_MillingMethodState)
    /// <summary>
    /// Stores an instance of the MillingMethodType Method.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class MillingMethodState : MethodState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public MillingMethodState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Constructs an instance of a node.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>The new node.</returns>
        public new static NodeState Construct(NodeState parent)
        {
            return new MillingMethodState(parent);
        }

        #if (!OPCUA_EXCLUDE_InitializationStrings)
        /// <summary>
        /// Initializes the instance.
        /// </summary>
        protected override void Initialize(ISystemContext context)
        {
            Initialize(context, InitializationString);
            InitializeOptionalChildren(context);
        }

        /// <summary>
        /// Initializes the any option children defined for the instance.
        /// </summary>
        protected override void InitializeOptionalChildren(ISystemContext context)
        {
            base.InitializeOptionalChildren(context);
        }

        #region Initialization String
        private const string InitializationString =
           "AwAAAC8AAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1pbGxpbmcuZnJhdW5ob2Zlci5kZScAAABo" +
           "dHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLmZyYXVuaG9mZXIuZGUgAAAAaHR0cDovL2NvbW1vbi5zd2Fw" +
           "LmZyYXVuaG9mZXIuZGX/////BGGCCgQAAAABABEAAABNaWxsaW5nTWV0aG9kVHlwZQEB0ToALwEB0TrR" +
           "OgAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAdI6AC4ARNI6AACWAQAAAAEA" +
           "KgEBFgAAAAUAAABvcmRlcgECnTr/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAABdgqQoC" +
           "AAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQHTOgAuAETTOgAAlgEAAAABACoBARgAAAAHAAAAUmVzdWx0" +
           "cwEDzDr/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        /// <summary>
        /// Raised when the the method is called.
        /// </summary>
        public MillingMethodStateMethodCallHandler OnCall;
        #endregion

        #region Public Properties
        #endregion

        #region Overridden Methods
        /// <summary>
        /// Invokes the method, returns the result and output argument.
        /// </summary>
        protected override ServiceResult Call(
            ISystemContext _context,
            NodeId _objectId,
            IList<object> _inputArguments,
            IList<object> _outputArguments)
        {
            if (OnCall == null)
            {
                return base.Call(_context, _objectId, _inputArguments, _outputArguments);
            }

            ServiceResult result = null;

            SWAP_Order order = (SWAP_Order)ExtensionObject.ToEncodeable((ExtensionObject)_inputArguments[0]);

            ServiceExecutionAsyncResultDataType results = (ServiceExecutionAsyncResultDataType)_outputArguments[0];

            if (OnCall != null)
            {
                result = OnCall(
                    _context,
                    this,
                    _objectId,
                    order,
                    ref results);
            }

            _outputArguments[0] = results;

            return result;
        }
        #endregion

        #region Private Fields
        #endregion
    }

    /// <summary>
    /// Used to receive notifications when the method is called.
    /// </summary>
    /// <exclude />
    public delegate ServiceResult MillingMethodStateMethodCallHandler(
        ISystemContext context,
        MethodState method,
        NodeId objectId,
        SWAP_Order order,
        ref ServiceExecutionAsyncResultDataType results);
    #endif
    #endregion
}