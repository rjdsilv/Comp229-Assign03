﻿<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Comp229_Assign03.Home" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="hero-image">
        <div class="black-transparency">
            <div class="container">
                <h1>College.Me</h1><br />
                <p>There is only one good, knowledge, and only one evil, ignorance.</p>
                <span>Socrates</span>
            </div>
        </div>
    </div>

    <div class="container body-content">
        <%-- Error Panel --%>
        <asp:Panel ID="ErrorPanel" runat="server" CssClass="school-error-message-hidden">
            <div><%= message %></div>
        </asp:Panel>

        <%-- Student success message panel --%>
        <asp:Panel ID="SuccessRemovalPanel" runat="server" CssClass="school-success-message-hidden">
            <div><%= message %></div>
        </asp:Panel>

        <%-- New Student Panel --%>
        <div class="row">
            <asp:Panel ID="StudentsInclusionPanel" runat="server" CssClass="school-panel">
                <div class="col-sm-12 school-panel-header"><span>New Student</span></div>
                <div class="col-sm-5">
                    <asp:TextBox ID="StudentFirstMidNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="First and Middle Names"/>
                </div>
                <div class="col-sm-5">
                    <asp:TextBox ID="StudentLastNameTextBox" TextMode="SingleLine" runat="server" CssClass="school-input" placeholder="Last Name" />
                </div>
                <div class="col-sm-2" style="margin-top:16px">
                    <asp:Button ID="StudentSaveButton" runat="server" CssClass="school-submit" Text=" Save Student " OnClick="StudentSaveButton_Click" />
                </div>
                <div class="col-sm-5">
                    <asp:RequiredFieldValidator ID="StudentFirstMidNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentFirstMidNameTextBox" ErrorMessage="First / Middle name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                </div>
                <div class="col-sm-5">
                    <asp:RequiredFieldValidator ID="StudentLastNameTextBox_RequiredFieldValidator" runat="server" ControlToValidate="StudentLastNameTextBox" ErrorMessage="Last name is required." SetFocusOnError="True" Display="Dynamic" CssClass="school-error-message"/>
                </div>
                <div class="col-sm-2">&nbsp</div>
            </asp:Panel>
        </div>
        <br />


        <%-- Student List --%>
        <div class="row table-header">
            <div class="col-sm-3"><span>Student Id</span></div>
            <div class="col-sm-4"><span>First and Middle Names</span></div>
            <div class="col-sm-3"><span>Last Name</span></div>
            <div class="col-sm-2"><span>Enrollment Date</span></div>
        </div>
        <asp:Repeater ID="StudentsRepeater" runat="server">
            <ItemTemplate>
                <div class="row table-row-light">
                    <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                        <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                        <div class="col-sm-4"><span><%# Eval("FirstMidName") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                        <div class="col-sm-2"><span><%# Eval("EnrollmentDate") %></span></div>
                    </a>
                </div>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <div class="row table-row-light-alternate">
                    <a href="StudentManagement.aspx?student=<%# Eval("Id") %>">
                        <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                        <div class="col-sm-4"><span><%# Eval("FirstMidName") %></span></div>
                        <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                        <div class="col-sm-2"><span><%# Eval("EnrollmentDate") %></span></div>
                    </a>
                </div>
            </AlternatingItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
