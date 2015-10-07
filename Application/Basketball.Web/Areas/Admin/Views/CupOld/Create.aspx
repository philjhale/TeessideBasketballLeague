<%@ Page Title="Create Cup" Language="C#" MasterPageFile="~/Views/Shared/Admin.Master" AutoEventWireup="true" 
    Inherits="System.Web.Mvc.ViewPage<Basketball.Core.Cup>" %>
<%@ Import Namespace="Basketball.Web.Controllers" %>
<%@ Import Namespace="Basketball.Web.Helpers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
Create Cup
</asp:Content>
<asp:Content ID="MainContent" ContentPlaceHolderID="MainContentPlaceHolder" runat="server">
    <% Html.RenderPartial("CupNav", ViewData); %>
    
    <h2>Create Cup</h2>

    <% Html.RenderPartial("CupForm", ViewData); %>
</asp:Content>
