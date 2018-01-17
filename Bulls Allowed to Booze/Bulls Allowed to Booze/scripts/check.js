function checkUID() {
    hideData();
    var uid = document.getElementById('txtUID').value.trim();
    
    if (uid != "") {
        document.getElementById('btnCheck').style.display = "none";
        document.getElementById('btnloader').style.display = "block";
        PageMethods.checkUID(uid, function (data) {
            console.log("success");
            if (data != "") {
                displayData(data);
                document.getElementById('btnCheck').style.display = "block";
                document.getElementById('btnloader').style.display = "none";
            }
            else {
                console.log("Not Registered Yet");
                document.getElementById('divUIDdetailsNotFound').style.display = "block";
                document.getElementById('btnCheck').style.display = "block";
                document.getElementById('btnloader').style.display = "none";
            }
        },
        function () {
            console.log("error");
            document.getElementById('divUIDdetailsNotFound').style.display = "block";
            document.getElementById('btnCheck').style.display = "block";
            document.getElementById('btnloader').style.display = "none";
        });
    }
    else {
        document.getElementById('err_txtUID').style.display = "block";
    }
}


function hideData() {
    document.getElementById('err_txtUID').style.display = "none";
    document.getElementById('divUIDdetailsNotFound').style.display = "none";
    document.getElementById('divNotVerified').style.display = "none";
    document.getElementById('divVerified').style.display = "none";
    document.getElementById('divNotLegal').style.display = "none";
    document.getElementById('divLegal').style.display = "none";
    document.getElementById('divUIDdetailsFound').style.display = "none";
}

function displayData(data) {

    var user_obj = JSON.parse(data);
    if (user_obj.uid_verified == true) {
        document.getElementById('divVerified').style.display = "block";
        if (user_obj.is_legal == true) {
            document.getElementById('divLegal').style.display = "block";
        }
        else {
            document.getElementById('divNotLegal').style.display = "block";
        }
        document.getElementById('txtFetchedUID').innerHTML = user_obj.uid;
        document.getElementById('txtGivenName').innerHTML = user_obj.given_name;
        document.getElementById('txtLastName').innerHTML = user_obj.last_name;
        document.getElementById('txtDateofBirth').innerHTML = user_obj.date_of_birth;
        document.getElementById('uid_image').src = user_obj.uid_image;
        document.getElementById('divUIDdetailsFound').style.display = "block";
    }
    else{
        document.getElementById('divNotVerified').style.display = "block";
    }

}