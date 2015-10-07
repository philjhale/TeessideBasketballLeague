<%@ Control Language="C#" AutoEventWireup="true" Inherits="System.Web.Mvc.ViewUserControl<Basketball.Core.Cup>" %>
<%@ Import Namespace="Basketball.Core" %>
<%@ Import Namespace="Basketball.Web.Controllers" %>
<%@ Import Namespace="Basketball.Core.Resources" %>
<%@ Import Namespace="Util.Mvc.Helpers" %>

<% using (Html.BeginForm()) { %>
    <%= Html.AntiForgeryToken() %>
    <%= Html.Hidden("id", (ViewData.Model != null) ? ViewData.Model.Id : 0) %>

	<p>
	    <label for="cup.CupName">Cup Name *</label><br />
        <%= Html.TextBox("cup.CupName",
              (ViewData.Model != null) ? ViewData.Model.CupName : "", new { maxlength = 30 })%>
        <%= Html.ValidationMessage("cup.CupName")%>
    </p>
    <p>
        <label for="leagueIds">Leagues</label><br />
        <%= Html.CheckBoxList("leagueIds", (List<CheckBoxListInfo>)ViewData["LeagueList"])%>
    </p>
    <p>
        <%= Html.SubmitButton("btnSave", "Save") %>
    </p>
    
    <% } %>
    
    <% 
    if (ViewData.Model != null && ViewData.Model.Id != null && ViewData.Model.Id > 0)
    {
        using (Html.BeginForm<CupController>(c => c.Delete(ViewData.Model.Id)))
        { %>
            <%= Html.AntiForgeryToken()%>
			<input type="submit" value="Delete" onclick="return confirm('<%=FormMessages.DeleteConfirm%>');" />
      <%}
    }%>
     

