/*
 * Licensed under the MIT License.
 * For details on the licensing terms, see the LICENSE file.
 * SPDX-License-Identifier: MIT
 *
 * Copyright 2023-2024 (c) Fraunhofer IOSB (Author: Florian Düwel)
 */

#ifndef OPEN62541_SERVICE_CALLBACKS_H
#define OPEN62541_SERVICE_CALLBACKS_H
#include <open62541/plugin/log_stdout.h>
#include "stdio.h"

UA_StatusCode warehousemethodCallback(UA_Server *server,
                                      const UA_NodeId *sessionId, void *sessionHandle,
                                      const UA_NodeId *methodId, void *methodContext,
                                      const UA_NodeId *objectId, void *objectContext,
                                      size_t inputSize, const UA_Variant *input,
                                      size_t outputSize, UA_Variant *output);

UA_StatusCode coatingmethodCallback(UA_Server *server,
                                      const UA_NodeId *sessionId, void *sessionHandle,
                                      const UA_NodeId *methodId, void *methodContext,
                                      const UA_NodeId *objectId, void *objectContext,
                                      size_t inputSize, const UA_Variant *input,
                                      size_t outputSize, UA_Variant *output);

UA_StatusCode millingmethodCallback(UA_Server *server,
                                    const UA_NodeId *sessionId, void *sessionHandle,
                                    const UA_NodeId *methodId, void *methodContext,
                                    const UA_NodeId *objectId, void *objectContext,
                                    size_t inputSize, const UA_Variant *input,
                                    size_t outputSize, UA_Variant *output);

UA_StatusCode gluingmethodCallback(UA_Server *server,
                                   const UA_NodeId *sessionId, void *sessionHandle,
                                   const UA_NodeId *methodId, void *methodContext,
                                   const UA_NodeId *objectId, void *objectContext,
                                   size_t inputSize, const UA_Variant *input,
                                   size_t outputSize, UA_Variant *output);

UA_StatusCode mountingmethodCallback(UA_Server *server,
                                     const UA_NodeId *sessionId, void *sessionHandle,
                                     const UA_NodeId *methodId, void *methodContext,
                                     const UA_NodeId *objectId, void *objectContext,
                                     size_t inputSize, const UA_Variant *input,
                                     size_t outputSize, UA_Variant *output);

#endif  // OPEN62541_SERVICE_CALLBACKS_H
