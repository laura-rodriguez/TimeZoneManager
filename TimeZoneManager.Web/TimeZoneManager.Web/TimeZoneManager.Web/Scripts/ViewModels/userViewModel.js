var User = function (id, email, password, confirmPassword, roles, availableRoles) {
    var self = this;

    self.ID = ko.observable(id || 0);
    self.Email = ko.observable(email || '');
    self.Password = ko.observable(password || '');
    self.ConfirmPassword = ko.observable(confirmPassword || '');
    self.Roles = ko.observableArray(roles || []);
    self.AvailableRoles = ko.observableArray(availableRoles || []);
    self.RolesAsString = ko.computed(function () { return self.Roles().join(", ") });
}

var UserViewModel = function () {
    var self = this;

    self.error = ko.observable();
    self.user = ko.observable(new User());
    self.newUser = ko.observable(new User());
    self.SearchText = ko.observable();
    self.availableRoles = ko.observableArray([]);
    self.isVisible = ko.observable(false);
    
    var allItems = ko.observableArray();

    self.users = ko.computed(function () {
        var text = self.SearchText() || '';
        var filter = text.toLowerCase();

        if (!filter) {
            return allItems();
        } else {
            return ko.utils.arrayFilter(allItems(), function (item) {
                return item.Email().toLowerCase().indexOf(filter) !== -1;
            });
        }
    }, this);




    /*************** Operations ****************/

    self.Init = function () {
        
        self.isVisible(false);
        self.error('');
        self.user(new User());
        self.newUser(new User());
        self.SearchText('');
        self.availableRoles([]);
        allItems([]);

        self.getAvailableRoles();
    }

    self.getAvailableRoles = function () {
        self.error('');

        APIService.doRequest('roles', 'GET').done(function (data) {
            $.each(data, function (index, value) {
                self.availableRoles.push(value);
            });

        }).fail(function (jqXHR, textStatus, errorThrown) {
            handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
        });
    }

    self.getAllUsers = function () {
        self.error('');
        allItems([]);

        APIService.doRequest('users', 'GET').done(function (data) {
            $.each(data, function (index, value) {
                allItems.push(new User(value.ID, value.Email, '', '', value.Roles, value.AvailableRoles));
            });

            self.SearchText('');
            self.isVisible(true);

        }).fail(function (jqXHR, textStatus, errorThrown) {
            handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
            self.isVisible(true);
        });
    }

    self.getUserDetail = function (item) {
        self.error('');

        APIService.doRequest('users/' + item.ID(), 'GET').done(function (data) {
            
            self.user(new User(data.ID, data.Email, '', '', data.Roles, data.AvailableRoles));
        }).fail(function (jqXHR, textStatus, errorThrown) {
            handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
        });
    }

    self.deleteUser = function (item) {
        self.error('');
        alertify.confirm('Are you sure you want to delete "' + item.Email() + '" user?',
               function (result) {
                   if (result) {
                       self.error('');

                       APIService.doRequest('users/' + item.ID(), 'DELETE').done(function (data) {
                           allItems.remove(item);
                       }).fail(function (jqXHR, textStatus, errorThrown) {
                           handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
                       });
                   }
               }
           );
    }

    self.updateUser = function (formElement) {
        self.error('');
        var item = self.user();

        APIService.doRequest('users/' + item.ID(), 'PUT', ko.toJSON(item)).done(function () {
            self.user(new User());
            allItems([]);
            self.getAllUsers();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
        });
    }

    self.addUser = function (formElement) {
        self.error('');
        if (self.newUser().Password() != self.newUser().ConfirmPassword()) {
            self.error('Password doesn\'t match');
        }
        else {
            APIService.doRequest('users', 'POST', ko.toJSON(self.newUser())).done(function (data) {
                allItems.push(new User(data.ID, data.Email, '', '', data.Roles, data.AvailableRoles));
                self.newUser(new User());
            }).fail(function (jqXHR, textStatus, errorThrown) {
                if (jqXHR.responseText)
                handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
            });
        }
    }
};
