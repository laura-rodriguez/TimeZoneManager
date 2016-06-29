var routeService = new function () {

    this.goToURL = function (uri) {
        return $.ajax({
            type: 'GET',
            url: uri,
        }).done(
            function (viewHTML) {
                $("#mainContainer").html(viewHTML);
            }
        );
    };
};