<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentManagement.aspx.cs" Inherits="Comp229_Assign03.StudentManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Error Panel --%>
    <asp:Panel ID="ErrorPanel" runat="server" CssClass="school-error-message-hidden">
        <div><%= errorMessage %></div>
    </asp:Panel>

    <% if (string.IsNullOrEmpty(Request.QueryString["student"]))
        { %>
        <%-- New Student Panel --%>
        <asp:Panel ID="StudentsInclusionPanel" runat="server" CssClass="school-panel">
            <div class="col-sm-12 school-panel-header"><span>New Student</span></div>
            <div class="col-sm-5">
                <asp:TextBox ID="StudentFirstMidNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="First and Middle Names"/>
            </div>
            <div class="col-sm-5">
                <asp:TextBox ID="StudentLastNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="Last Name" />
            </div>
            <div class="col-sm-2" style="margin-top:16px">
                <asp:Button ID="StudentSaveButton" TextMode="SingleLine" runat="server" CssClass="school-submit" Text=" Save Student " OnClick="StudentSaveButton_Click" />
            </div>
            <div class="col-sm-5">
                <asp:RequiredFieldValidator ID="StudentFirstMidNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentFirstMidNameTextBox" ErrorMessage="First / Middle name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
            </div>
            <div class="col-sm-5">
                <asp:RequiredFieldValidator ID="StudentLastNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentLastNameTextBox" ErrorMessage="Last name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
            </div>
            <div class="col-sm-2">&nbsp</div>
        </asp:Panel>

        <%-- Student List --%>
        <div class="row table-header">
            <div class="col-sm-3"><span>Student Id</span></div>
            <div class="col-sm-3"><span>First and Middle Names</span></div>
            <div class="col-sm-3"><span>Last Name</span></div>
            <div class="col-sm-3"><span>Enrollment Date</span></div>
        </div>
        <asp:Repeater ID="StudentsRepeater" runat="server">
            <ItemTemplate>
                <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                    <div class="row table-row">
                        <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("FirstMidName") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("EnrollmentDate") %></span></div>
                    </div>
                </a>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                    <div class="row table-row-alternate">
                        <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("FirstMidName") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("EnrollmentDate") %></span></div>
                    </div>
                </a>
            </AlternatingItemTemplate>
        </asp:Repeater>
    <% } else { %>
        <asp:Panel ID="StudentDetailsPanel" runat="server" CssClass="school-panel">
            <div class="col-sm-12 school-panel-header"><span>Student <%= Request.QueryString["student"] %> Details</span></div>
        </asp:Panel>
    <% } %>
</asp:Content>
