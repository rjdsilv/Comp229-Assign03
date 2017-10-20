<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Comp229_Assign03._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%-- New Student Panel --%>
    <asp:Panel ID="StudentsInclusionPanel" runat="server" CssClass="school-new-student-panel">
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
            <a href="Student.aspx?student=<%# Eval("Id") %>">
                <div class="row table-row">
                    <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("FirstMidName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("EnrollmentDate") %></span></div>
                </div>
            </a>
        </ItemTemplate>
        <AlternatingItemTemplate>
            <a href="Student.aspx?student=<%# Eval("Id") %>">
                <div class="row table-row-alternate">
                    <div class="col-sm-3"><span><%# Eval("Id") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("FirstMidName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("LastName") %></span></div>
                    <div class="col-sm-3"><span><%# Eval("EnrollmentDate") %></span></div>
                </div>
            </a>
        </AlternatingItemTemplate>
    </asp:Repeater>
</asp:Content>
