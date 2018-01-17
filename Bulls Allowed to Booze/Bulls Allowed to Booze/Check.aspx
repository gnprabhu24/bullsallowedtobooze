<%@ Page Language="C#" MasterPageFile="~/BullsAllowedtoBooze.Master" AutoEventWireup="true" CodeBehind="Check.aspx.cs" Inherits="Bulls_Allowed_to_Booze.Check" %>

<asp:Content ID="Checkform" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
        <div class="form-class">
            <div class="form-class-heading">UID Search:</div>
             <div>
                <label><span>Enter UID:</span>
                <input class="input-field" type="text" id="txtUID" name="txtUID" /></label>
            </div>
             <div id="err_txtUID" class="err-div">
                <label><span>&nbsp;</span>Please enter an UID to Search.</label>
            </div>
             <div>
                 <label><span>&nbsp;</span></label>
                 <button type="button" id="btnCheck" onclick="javascript:checkUID();">Check</button>
                <img src="images/loader.gif" id="btnloader" style="display:none; width:auto" />
            </div>
            <div id="divVerified" style="display: none;">
                <label><span>&nbsp;</span></label><h4 style="color:forestgreen;">UID is Verified.</h4>
            </div>
             <div id="divNotVerified" style="display: none;">
                <label><span>&nbsp;</span></label><h4 style="color:red;">UID is Not Verified.</h4>
            </div>
             <div id="divUIDdetailsNotFound" style="display: none;">
                <label><span>&nbsp;</span></label><h4 style="color:red;">Not Registered with Bulls Allowed to Booze</h4>
            </div>
            <div id="divUIDdetailsFound" style="display: none;">
    
                <div class="form-class-heading" style="margin-top: 20px;">UID Information:</div>
    
                <div id="divLegal" style="display: none;">
                    <label><span>&nbsp;</span></label><h4 style="color:forestgreen;">Cheers! This UID is LEGAL</h4>
                </div>
                 <div id="divNotLegal" style="display: none;">
                    <label><span>&nbsp;</span></label><h4 style="color:red;">Sorry! This UID is not yet 21 years old.</h4>
                </div>

                <div>
                    <label><span>UID:</span><p id="txtFetchedUID"></p></label>
                </div>
            
                <div>
                    <label><span>Given Name:</span><p id="txtGivenName"></p></label>
                </div>

                <div>
                    <label><span>Last Name:</span><p id="txtLastName"></p></label>
                </div>

                <div>
                    <label><span>Date of Birth:</span><p id="txtDateofBirth"></p></label>
                </div>

                <div>
                    <label><span>Photograph:</span></label>
                    <div>
                        <span>&nbsp;</span>
                        <img id="uid_image" style="width: 200px;"/>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>
<asp:Content ID="Checkscript" ContentPlaceHolderID="scripts" runat="server">
    <!-- Custom Scripts -->
    <script  src="scripts/check.js" type="text/javascript"></script>
</asp:Content>
