<%@ Page Language="C#" MasterPageFile="~/BullsAllowedtoBooze.Master" AutoEventWireup="true" CodeBehind="Verify.aspx.cs" Inherits="Bulls_Allowed_to_Booze.Verify" %>

<asp:Content ID="Verifyform" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
         <div class="form-class" style="min-height: calc(100vh - 167px);" >
            <div class="form-class-heading">Verification of UID Details</div>
            <div>
                <label><span>UID:</span>
                <label id="lblUID" style="" runat="server"></label>
                </label>
                <input type="hidden" id="txtUID" name="txtUID" runat="server" />
            </div>
            <div>
                <label><span>&nbsp;</span></label>
                <button type="button" id="btnVerify" onclick="javascript:confirmVerifyUID(true);">Validate</button>
            </div>
            <div>
                <label><span>&nbsp;</span>OR</label>
            </div>
            <div>
                <label><span>Reason for Invalid Registeration:</span>
                <textarea class="textarea-field" id="txtComment"></textarea></label>
            </div>
             <div id="err_txtComment" class="err-div">
                <label><span>&nbsp;</span>Please provide a reason for Invalidation.</label>
            </div>
             <div>
                 <label><span>&nbsp;</span></label>
                 <button type="button" id="btnUnVerify" onclick="javascript:confirmVerifyUID(false);">Invalidate</button>
             </div>
        </div>
    </form>
    <div id="confirmModal" title="Confirmation">
      <p id="txtConfirm"></p>
    </div>

</asp:Content>
<asp:Content ID="Verifyscript" ContentPlaceHolderID="scripts" runat="server">
    <!-- Custom Scripts -->
    <script  src="scripts/verify.js" type="text/javascript"></script>
</asp:Content>