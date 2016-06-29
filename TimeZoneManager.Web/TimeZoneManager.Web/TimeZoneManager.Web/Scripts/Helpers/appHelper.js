var loadingCounter = 0;

function showLoading() {
    loadingCounter++;
    $(".loadingContainer").show();
}

function hideLoading(force) {
    if (force) //Hide loading without taking consideration of counting.
        loadingCounter = 1;
    loadingCounter--;

    if (loadingCounter <= 0) {
        loadingCounter = 0;
        $(".loadingContainer").hide();
    }
}

function logout(redirectURL) {
    localStorage.setItem("api_token", "undefined");
    cleanUserInfo();
    routeService.goToURL(redirectURL);
}

function handleAPIFail(statusCode, koViewModel, errorThrown, responseTextJSON) {
    if (statusCode == 401) {
        routeService.goToURL('/Account/Login');
    }
    else if (statusCode == 403) {
        routeService.goToURL('/Static/Forbidden.html');
    }
    else {
        var error = errorThrown;
       
        if (error != "") {
            error += ". ";
        }

        error +=  getErrors(responseTextJSON);
        koViewModel.error(error);
    }
}

function getErrors(responseTextJSON) {
    var error = "";
    if (responseTextJSON) {
        if (responseTextJSON.ExceptionMessage != null && responseTextJSON.ExceptionMessage != "") {
            error += responseTextJSON.ExceptionMessage;
        }
        if (responseTextJSON.Message != null && responseTextJSON.Message != "") {
            error += responseTextJSON.Message;
        }
    }

    return error;
}

function setUserInfo(username, roles) {
    
    localStorage.setItem("user_info", JSON.stringify({ Username: username, Roles: roles }));
    updateHeader();
}

function getUserInfo() {
    
    return JSON.parse(localStorage.getItem("user_info"));
}

function cleanUserInfo(username, roles) {
    localStorage.setItem("user_info", JSON.stringify({}));
    updateHeader();
}

function updateHeader() {
    
    var userInfo = getUserInfo();

    if ($.isEmptyObject(userInfo)) {
        $("#registerLink > a").show();
        $("#loginLink > a").show();
        $("#logoutLink > a").hide();
        $("#timeZonesLink > a").hide();
        $("#usersLink > a").hide();
    } else {
        $("#registerLink > a").hide();
        $("#loginLink > a").hide();
        $("#logoutLink > a").show();

        if (userInfo.Roles.indexOf('UserManager') != -1 || userInfo.Roles.indexOf('Admin') != -1) {
            $("#usersLink > a").show();
        } else {
            $("#usersLink > a").hide();
        }

        if (userInfo.Roles.indexOf('User') != -1 || userInfo.Roles.indexOf('Admin') != -1) {
            $("#timeZonesLink > a").show();
        } else {
            $("#timeZonesLink > a").hide();
        }

    }
}

function redirectOnLogin() {
    var userInfo = getUserInfo();
    if (userInfo.Roles.indexOf('User') != -1 || userInfo.Roles.indexOf('Admin') != -1) {
        routeService.goToURL('/TimeZone/Index');
    } else {
        routeService.goToURL('/User/Index');
    }
}

