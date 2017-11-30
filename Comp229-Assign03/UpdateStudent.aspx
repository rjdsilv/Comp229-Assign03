<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UpdateStudent.aspx.cs" Inherits="Comp229_Assign03.UpdateStudent" %>
<asp:Content ID="UpdateStudentContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container body-content">
        <%-- Error Panel --%>
        <asp:Panel ID="ErrorPanel" runat="server" CssClass="school-error-message-hidden">
            <div><%= message %></div>
        </asp:Panel>

        <%-- Student success message panel --%>
        <asp:Panel ID="SuccessUpdatePanel" runat="server" CssClass="school-success-message-hidden">
            <div><%= message %></div>
        </asp:Panel>

        <% if (!string.IsNullOrEmpty(Request.QueryString["student"])) { %>
            <%-- New Student Panel --%>
            <asp:Panel ID="StudentUpdatePanel" runat="server" CssClass="school-panel">
                <div class="col-sm-12 school-panel-header"><span>Student Details</span></div>
                <div class="col-sm-6">
                    <asp:TextBox ID="StudentID" TextMode="SingleLine" ReadOnly="true" Enabled="false" runat="server" CssClass="school-input" placeholder="StudentID"/>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="StudentEnrollmentDateTextBox" TextMode="Date" runat="server" CssClass="school-input" placeholder="Enrollment Date" />
                </div>
                <div class="col-sm-6"></div>
                <div class="col-sm-6">
                    <asp:CompareValidator ID="StudentEnrollmentDateTextBox_CompareFieldValidator" Type="Date" Operator="DataTypeCheck" ControlToValidate="StudentEnrollmentDateTextBox" ErrorMessage="Please, enter a valid date." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message" runat="server" />
                    <asp:RequiredFieldValidator ID="StudentEnrollmentDateTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentEnrollmentDateTextBox" ErrorMessage="Enrollment date is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                </div>

                <div class="col-sm-6">
                    <asp:TextBox ID="StudentFirstMidNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="First and Middle Names"/>
                </div>
                <div class="col-sm-6">
                    <asp:TextBox ID="StudentLastNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="Last Name" />
                </div>
                <div class="col-sm-6">
                    <asp:RequiredFieldValidator ID="StudentFirstMidNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentFirstMidNameTextBox" ErrorMessage="First / Middle name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                </div>
                <div class="col-sm-6">
                    <asp:RequiredFieldValidator ID="StudentLastNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentLastNameTextBox" ErrorMessage="Last name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                </div>
            </asp:Panel>
            <div class="col-sm-1"></div>
            <div class="col-sm-10 button-area">
                <asp:Button ID="StudentUpdateButton" runat="server" CssClass="school-submit" Text=" Update Student " OnClick="StudentUpdateButton_Click" />
            </div>
            <div class="col-sm-1"></div>
        <% } %>
    </div>
</asp:Content>
