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

namespace SWAP.Fraunhofer.Demo.Scenario.Mounting.Model
{
    #region MountingState Class
    #if (!OPCUA_EXCLUDE_MountingState)
    /// <summary>
    /// Stores an instance of the Mounting ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class MountingState : ServiceFinishedEventState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public MountingState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(SWAP.Fraunhofer.Demo.Scenario.Mounting.Model.ObjectTypes.Mounting, SWAP.Fraunhofer.Demo.Scenario.Mounting.Model.Namespaces.Mounting, namespaceUris);
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
           "AwAAADAAAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1vdW50aW5nLmZyYXVuaG9mZXIuZGUnAAAA" +
           "aHR0cDovL3N3YXAuZGVtby5zY2VuYXJpby5mcmF1bmhvZmVyLmRlIAAAAGh0dHA6Ly9jb21tb24uc3dh" +
           "cC5mcmF1bmhvZmVyLmRl/////wRggAIBAAAAAQAQAAAATW91bnRpbmdJbnN0YW5jZQEBmToBAZk6mToA" +
           "AP////8MAAAAFWCJCgIAAAAAAAcAAABFdmVudElkAQGaOgAuAESaOgAAAA//////AQH/////AAAAABVg" +
           "iQoCAAAAAAAJAAAARXZlbnRUeXBlAQGbOgAuAESbOgAAABH/////AQH/////AAAAABVgiQoCAAAAAAAK" +
           "AAAAU291cmNlTm9kZQEBnDoALgBEnDoAAAAR/////wEB/////wAAAAAVYIkKAgAAAAAACgAAAFNvdXJj" +
           "ZU5hbWUBAZ06AC4ARJ06AAAADP////8BAf////8AAAAAFWCJCgIAAAAAAAQAAABUaW1lAQGeOgAuAESe" +
           "OgAAAQAmAf////8BAf////8AAAAAFWCJCgIAAAAAAAsAAABSZWNlaXZlVGltZQEBnzoALgBEnzoAAAEA" +
           "JgH/////AQH/////AAAAABVgiQoCAAAAAAAHAAAATWVzc2FnZQEBoToALgBEoToAAAAV/////wEB////" +
           "/wAAAAAVYIkKAgAAAAAACAAAAFNldmVyaXR5AQGiOgAuAESiOgAAAAX/////AQH/////AAAAABVgiQoC" +
           "AAAAAwAWAAAAU2VydmljZUV4ZWN1dGlvblJlc3VsdAEBozoALgBEozoAAAEDwjr/////AQH/////AAAA" +
           "ABVgiQoCAAAAAwAMAAAAc2VydmljZV91dWlkAQGkOgAuAESkOgAAAAz/////AQH/////AAAAABVgiQoC" +
           "AAAAAwAJAAAAdGFza191dWlkAQGlOgAuAESlOgAAAAz/////AQH/////AAAAABVgiQoCAAAAAQAFAAAA" +
           "b3JkZXIBAaY6AC4ARKY6AAABAp06/////wEB/////wAAAAA=";
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
                case SWAP.Fraunhofer.Demo.Scenario.Mounting.Model.BrowseNames.order:
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
            return Opc.Ua.NodeId.Create(SWAP.Fraunhofer.Demo.Scenario.Mounting.Model.ObjectTypes.ServiceObjectType, SWAP.Fraunhofer.Demo.Scenario.Mounting.Model.Namespaces.Mounting, namespaceUris);
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
           "AwAAADAAAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1vdW50aW5nLmZyYXVuaG9mZXIuZGUnAAAA" +
           "aHR0cDovL3N3YXAuZGVtby5zY2VuYXJpby5mcmF1bmhvZmVyLmRlIAAAAGh0dHA6Ly9jb21tb24uc3dh" +
           "cC5mcmF1bmhvZmVyLmRl/////wRggAIBAAAAAQAZAAAAU2VydmljZU9iamVjdFR5cGVJbnN0YW5jZQEB" +
           "pzoBAac6pzoAAP////8EAAAABGGCCgQAAAADAAgAAAByZWdpc3RlcgEBqDoALwED4jqoOgAAAQH/////" +
           "AQAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAak6AC4ARKk6AACWAgAAAAEAKgEBGwAAAAwA" +
           "AABSZWdpc3RyeSBVUkwADP////8AAAAAAAEAKgEBGwAAAAwAAABSZXNvdXJjZSBVUkwADP////8AAAAA" +
           "AAEAKAEBAAAAAQAAAAAAAAABAf////8AAAAABGGCCgQAAAADAAoAAAB1bnJlZ2lzdGVyAQGqOgAvAQPk" +
           "Oqo6AAABAf////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBqzoALgBEqzoAAJYCAAAA" +
           "AQAqAQEbAAAADAAAAFJlZ2lzdHJ5IFVSTAAM/////wAAAAAAAQAqAQEbAAAADAAAAFJlc291cmNlIFVS" +
           "TAAM/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAVYIkKAgAAAAMACgAAAHJlZ2lzdGVy" +
           "ZWQBAaw6AC8AP6w6AAAAAf////8DA/////8AAAAABGGCCgQAAAADAAgAAABNb3VudGluZwEBrToALwEB" +
           "rTqtOgAAAQH/////AgAAABdgqQoCAAAAAAAOAAAASW5wdXRBcmd1bWVudHMBAa46AC4ARK46AACWAQAA" +
           "AAEAKgEBFgAAAAUAAABvcmRlcgECnTr/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAABdg" +
           "qQoCAAAAAAAPAAAAT3V0cHV0QXJndW1lbnRzAQGvOgAuAESvOgAAlgEAAAABACoBARgAAAAHAAAAUmVz" +
           "dWx0cwEDzDr/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAAA==";
        #endregion
        #endif
        #endregion

        #region Public Properties
        /// <remarks />
        public MountingMethodState Mounting
        {
            get
            {
                return m_mountingMethod;
            }

            set
            {
                if (!Object.ReferenceEquals(m_mountingMethod, value))
                {
                    ChangeMasks |= NodeStateChangeMasks.Children;
                }

                m_mountingMethod = value;
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
            if (m_mountingMethod != null)
            {
                children.Add(m_mountingMethod);
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
                case SWAP.Fraunhofer.Common.Model.BrowseNames.Mounting:
                {
                    if (createOrReplace)
                    {
                        if (Mounting == null)
                        {
                            if (replacement == null)
                            {
                                Mounting = new MountingMethodState(this);
                            }
                            else
                            {
                                Mounting = (MountingMethodState)replacement;
                            }
                        }
                    }

                    instance = Mounting;
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
        private MountingMethodState m_mountingMethod;
        #endregion
    }
    #endif
    #endregion

    #region MountingModuleState Class
    #if (!OPCUA_EXCLUDE_MountingModuleState)
    /// <summary>
    /// Stores an instance of the MountingModuleType ObjectType.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class MountingModuleState : ModuleState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public MountingModuleState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Returns the id of the default type definition node for the instance.
        /// </summary>
        protected override NodeId GetDefaultTypeDefinitionId(NamespaceTable namespaceUris)
        {
            return Opc.Ua.NodeId.Create(SWAP.Fraunhofer.Demo.Scenario.Mounting.Model.ObjectTypes.MountingModuleType, SWAP.Fraunhofer.Demo.Scenario.Mounting.Model.Namespaces.Mounting, namespaceUris);
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
           "AwAAADAAAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1vdW50aW5nLmZyYXVuaG9mZXIuZGUnAAAA" +
           "aHR0cDovL3N3YXAuZGVtby5zY2VuYXJpby5mcmF1bmhvZmVyLmRlIAAAAGh0dHA6Ly9jb21tb24uc3dh" +
           "cC5mcmF1bmhvZmVyLmRl/////wRggAIBAAAAAQAaAAAATW91bnRpbmdNb2R1bGVUeXBlSW5zdGFuY2UB" +
           "AbA6AQGwOrA6AAD/////BgAAAARggAoBAAAAAwAMAAAAQ2FwYWJpbGl0aWVzAQGxOgAvADqxOgAA////" +
           "/wAAAAAEYIAKAQAAAAMACgAAAFByb3BlcnRpZXMBAbI6AC8AOrI6AAD/////AwAAAARggAoBAAAAAwAF" +
           "AAAAQXNzZXQBAbM6AC8AOrM6AAD/////AgAAABVgiQoCAAAAAwAMAAAAU2VyaWFsTnVtYmVyAQG0OgAv" +
           "AD+0OgAAAAz/////AwP/////AAAAABVgiQoCAAAAAwAGAAAAVmVuZG9yAQG1OgAvAD+1OgAAAAz/////" +
           "AwP/////AAAAAARggAoBAAAAAwAKAAAAQXNzZXRDbGFzcwEBtjoALwA6tjoAAP////8EAAAAFWCJCgIA" +
           "AAADABIAAABDb21tb25Nb2RlbFZlcnNpb24BAbc6AC8AP7c6AAAADP////8DA/////8AAAAAFWCJCgIA" +
           "AAADABIAAABNb2R1bGVPcmdhbml6YXRpb24BAbg6AC8AP7g6AAABA7o6/////wMD/////wAAAAAVYIkK" +
           "AgAAAAMAEQAAAE1vZHVsZVByb2ZpbGVOYW1lAQG5OgAvAD+5OgAAAAz/////AwP/////AAAAABVgiQoC" +
           "AAAAAwAUAAAATW9kdWxlUHJvZmlsZVZlcnNpb24BAbo6AC8AP7o6AAAABP////8DA/////8AAAAAFWCJ" +
           "CgIAAAADAAoAAABTdGF0aW9uYXJ5AQG7OgAvAD+7OgAAAAH/////AwP/////AAAAAARggAoBAAAAAwAF" +
           "AAAAUXVldWUBAbw6AC8AOrw6AAD/////AQAAAARggAoBAAAAAwAMAAAAU2VydmljZVF1ZXVlAQG9OgAv" +
           "AQPbOr06AAD/////AwAAABVgiQoCAAAAAwAOAAAAcXVldWVfdmFyaWFibGUBAb46AC8AP746AAABA8s6" +
           "/////wMD/////wAAAAAEYYIKBAAAAAMAEQAAAGFkZF9xdWV1ZV9lbGVtZW50AQG/OgAvAQPdOr86AAAB" +
           "Af////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBwDoALgBEwDoAAJYBAAAAAQAqAQEa" +
           "AAAACQAAAG5ldyBpbnB1dAEDyzr/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAAARhggoE" +
           "AAAAAwAUAAAAcmVtb3ZlX3F1ZXVlX2VsZW1lbnQBAcE6AC8BA986wToAAAEB/////wEAAAAXYKkKAgAA" +
           "AAAADgAAAElucHV0QXJndW1lbnRzAQHCOgAuAETCOgAAlgEAAAABACoBAR8AAAAOAAAAcmVtb3ZlX2Vs" +
           "ZW1lbnQBA8s6/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAEYIAKAQAAAAMAFgAAAFJl" +
           "Z2lzdHJ5X1N1YnNjcmlwdGlvbnMBAcM6AC8AOsM6AAD/////AQAAABVgiQoCAAAAAwAUAAAAU3Vic2Ny" +
           "aXB0aW9uX09iamVjdHMBAcQ6AC8AP8Q6AAAAFP////8DA/////8AAAAABGCACgEAAAADAAgAAABTZXJ2" +
           "aWNlcwEBxToALwEBpzrFOgAA/////wQAAAAEYYIKBAAAAAMACAAAAHJlZ2lzdGVyAQHGOgAvAQPiOsY6" +
           "AAABAf////8BAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBxzoALgBExzoAAJYCAAAAAQAq" +
           "AQEbAAAADAAAAFJlZ2lzdHJ5IFVSTAAM/////wAAAAAAAQAqAQEbAAAADAAAAFJlc291cmNlIFVSTAAM" +
           "/////wAAAAAAAQAoAQEAAAABAAAAAAAAAAEB/////wAAAAAEYYIKBAAAAAMACgAAAHVucmVnaXN0ZXIB" +
           "Acg6AC8BA+Q6yDoAAAEB/////wEAAAAXYKkKAgAAAAAADgAAAElucHV0QXJndW1lbnRzAQHJOgAuAETJ" +
           "OgAAlgIAAAABACoBARsAAAAMAAAAUmVnaXN0cnkgVVJMAAz/////AAAAAAABACoBARsAAAAMAAAAUmVz" +
           "b3VyY2UgVVJMAAz/////AAAAAAABACgBAQAAAAEAAAAAAAAAAQH/////AAAAABVgiQoCAAAAAwAKAAAA" +
           "cmVnaXN0ZXJlZAEByjoALwA/yjoAAAAB/////wMD/////wAAAAAEYYIKBAAAAAMACAAAAE1vdW50aW5n" +
           "AQHOOgAvAQGtOs46AAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEBzzoALgBE" +
           "zzoAAJYBAAAAAQAqAQEWAAAABQAAAG9yZGVyAQKdOv////8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf//" +
           "//8AAAAAF2CpCgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAdA6AC4ARNA6AACWAQAAAAEAKgEBGAAA" +
           "AAcAAABSZXN1bHRzAQPMOv////8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf////8AAAAABGCACgEAAAAD" +
           "AAUAAABTdGF0ZQEByzoALwA6yzoAAP////8CAAAAFWCJCgIAAAADAAoAAABBc3NldFN0YXRlAQHMOgAv" +
           "AD/MOgAAAQO8Ov////8DA/////8AAAAAFWCJCgIAAAADAAgAAABMb2NhdGlvbgEBzToALwA/zToAAAAM" +
           "/////wMD/////wAAAAA=";
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

    #region MountingMethodState Class
    #if (!OPCUA_EXCLUDE_MountingMethodState)
    /// <summary>
    /// Stores an instance of the MountingMethodType Method.
    /// </summary>
    /// <exclude />
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Opc.Ua.ModelCompiler", "1.0.0.0")]
    public partial class MountingMethodState : MethodState
    {
        #region Constructors
        /// <summary>
        /// Initializes the type with its default attribute values.
        /// </summary>
        public MountingMethodState(NodeState parent) : base(parent)
        {
        }

        /// <summary>
        /// Constructs an instance of a node.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns>The new node.</returns>
        public new static NodeState Construct(NodeState parent)
        {
            return new MountingMethodState(parent);
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
           "AwAAADAAAABodHRwOi8vc3dhcC5kZW1vLnNjZW5hcmlvLm1vdW50aW5nLmZyYXVuaG9mZXIuZGUnAAAA" +
           "aHR0cDovL3N3YXAuZGVtby5zY2VuYXJpby5mcmF1bmhvZmVyLmRlIAAAAGh0dHA6Ly9jb21tb24uc3dh" +
           "cC5mcmF1bmhvZmVyLmRl/////wRhggoEAAAAAQASAAAATW91bnRpbmdNZXRob2RUeXBlAQHROgAvAQHR" +
           "OtE6AAABAf////8CAAAAF2CpCgIAAAAAAA4AAABJbnB1dEFyZ3VtZW50cwEB0joALgBE0joAAJYBAAAA" +
           "AQAqAQEWAAAABQAAAG9yZGVyAQKdOv////8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf////8AAAAAF2Cp" +
           "CgIAAAAAAA8AAABPdXRwdXRBcmd1bWVudHMBAdM6AC4ARNM6AACWAQAAAAEAKgEBGAAAAAcAAABSZXN1" +
           "bHRzAQPMOv////8AAAAAAAEAKAEBAAAAAQAAAAAAAAABAf////8AAAAA";
        #endregion
        #endif
        #endregion

        #region Event Callbacks
        /// <summary>
        /// Raised when the the method is called.
        /// </summary>
        public MountingMethodStateMethodCallHandler OnCall;
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
    public delegate ServiceResult MountingMethodStateMethodCallHandler(
        ISystemContext context,
        MethodState method,
        NodeId objectId,
        SWAP_Order order,
        ref ServiceExecutionAsyncResultDataType results);
    #endif
    #endregion
}