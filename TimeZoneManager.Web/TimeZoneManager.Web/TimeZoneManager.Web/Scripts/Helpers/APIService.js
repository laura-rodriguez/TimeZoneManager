var APIService = new function () {

    var baseURL = 'http://localhost:3822/';

    this.doRequest = function (uri, method, data) {

        var apiToken = localStorage.getItem('api_token');

        if (!apiToken || apiToken === 'undefined') {
            cleanUserInfo();
            routeService.goToURL('/Account/Login');
        }

        return $.ajax({
            headers: { 'Authorization': 'Bearer ' + apiToken },
            type: method,
            url: baseURL + 'api/' + uri,
            dataType: 'json',
            contentType: 'application/json',
            data: (data || '')
        });
    };

    this.getToken = function (username, password) {
        return $.ajax({
            type: 'POST',
            url: baseURL + 'token',
            contentType: 'application/x-www-form-urlencoded',
            data: 'grant_type=password&Username=' + encodeURIComponent(username) + '&Password=' + encodeURIComponent(password)
        });
    };

    this.getUserInfo = function () {

        var apiToken = localStorage.getItem('api_token');

        if (!apiToken || apiToken === 'undefined') {
            cleanUserInfo();
            routeService.goToURL('/Account/Login');
        }

        return $.ajax({
            headers: { 'Authorization': 'Bearer ' + apiToken },
            type: 'GET',
            url: baseURL + 'api/me',
            dataType: 'json'
        });
    };

    this.register = function (username, password, confirmPassword) {
        var registerJSON = { Email: username, Password: password, ConfirmPassword: confirmPassword };

        return $.ajax({
            type: 'POST',
            url: baseURL + 'api/account/register',
            contentType: 'application/json',
            data: JSON.stringify(registerJSON)
        });
    };
};