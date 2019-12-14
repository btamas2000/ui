// $begin{copyright}
//
// This file is part of WebSharper
//
// Copyright (c) 2008-2014 IntelliFactory
//
// Licensed under the Apache License, Version 2.0 (the "License"); you
// may not use this file except in compliance with the License.  You may
// obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or
// implied.  See the License for the specific language governing
// permissions and limitations under the License.
//
// $end{copyright}

namespace WebSharper.UI.Next

open Microsoft.FSharp.Quotations
open WebSharper.JavaScript
module M = WebSharper.Core.Metadata

/// A potentially time-varying or animated attribute list.
type Attr =
    internal
    | AppendAttr of list<Attr>
    | SingleAttr of string * string
    | DepAttr of string * (M.Info -> string) * seq<M.Node>

    interface WebSharper.IRequiresResources

    member Write : M.Info * WebSharper.Core.Resources.HtmlTextWriter * bool -> unit

    /// Sets a basic DOM attribute, such as `id` to a text value.
    static member Create : name: string -> value: string -> Attr

  // Note: Empty, Append, Concat define a monoid on Attr.

    /// Append on attributes.
    static member Append : Attr -> Attr -> Attr

    /// Concatenation on attributes.
    static member Concat : seq<Attr> -> Attr

    /// Empty attribute list.
    static member Empty : Attr

    /// Sets an event handler, for a given event such as `click`.
    /// When called on the server side, the handler must be a top-level function or a static member.
    static member Handler : event: string -> callback: (Expr<Dom.Element -> #Dom.Event -> unit>) -> Attr

    /// Sets an event handler, for a given event such as `click`.
    /// When called on the server side, the handler must be a top-level function or a static member.
    static member HandlerLinq : event: string -> callback: (System.Linq.Expressions.Expression<System.Action<Dom.Element, #Dom.Event>>) -> Attr
