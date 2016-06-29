var TimeZone = function (id, name, owner, city, gmtOffset) {
    var self = this;

    self.ID = ko.observable(id || 0);
    self.Name = ko.observable(name || '');
    self.Owner = ko.observable(owner || '');
    self.City = ko.observable(city || '');
    self.GMTOffset = ko.observable(gmtOffset || 0);
    self.CurrentTime = ko.observable();

    self.tick = function () {
        var newDate = moment(new Date()).utcOffset(self.GMTOffset()).format('YYYY-MM-DD HH:mm:ss');
        self.CurrentTime(newDate);
    };

    setInterval(self.tick, 1000);
}

var TimeZonesViewModel = function () {
    var self = this;

    self.error = ko.observable();
    self.detail = ko.observable();
    self.timeZone = ko.observable(new TimeZone());
    self.newTimeZone = ko.observable(new TimeZone());
    self.SearchText = ko.observable();
    self.isVisible = ko.observable(false);

    var allItems = ko.observableArray();

    self.timeZones = ko.computed(function () {
        var text = self.SearchText() || '';
        var filter = text.toLowerCase();

        if (!filter) {
            return allItems();
        } else {
            return ko.utils.arrayFilter(allItems(), function (item) {
                return item.Name().toLowerCase().indexOf(filter) !== -1;
            });
        }
    }, this);

    

    
   /*************** Operations ****************/

    self.Init = function () {
        
        self.isVisible(false);
        self.error('');
        self.detail('');
        self.timeZone(new TimeZone());
        self.newTimeZone(new TimeZone());
        self.SearchText('');
        allItems([]);
    }

    self.getAllTimeZones = function () {
        

        self.error('');
        allItems([]);

        APIService.doRequest('timezones', 'GET').done(function (data) {
            $.each(data, function (index, value) {
                allItems.push(new TimeZone(value.ID, value.Name, value.Owner, value.City, value.GMTOffset));
            });

            self.SearchText('');
            self.isVisible(true);
        }).fail(function (jqXHR, textStatus, errorThrown) {
            handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
            self.isVisible(true);
        });
    }

   self.getTimeZoneDetail = function (item) {
       self.error('');

       APIService.doRequest('timezones/' + item.ID(), 'GET').done(function (data) {
           self.timeZone(new TimeZone(data.ID, data.Name, data.Owner, data.City, data.GMTOffset));
       }).fail(function (jqXHR, textStatus, errorThrown) {
           
           handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
       });
    }

   self.deleteTimeZone = function (item) {
       self.error('');
       alertify.confirm('Are you sure you want to delete "' + item.Name() + '" user?',
                   function (result) {
                       if (result) {
                           self.error('');

                           APIService.doRequest('timezones/' + item.ID(), 'DELETE').done(function (data) {
                               allItems.remove(item);
                           }).fail(function (jqXHR, textStatus, errorThrown) {
                               
                               handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
                           });
                       }
                   }
               );
    }

    self.updateTimeZone = function (formElement) {
        self.error('');
        var item = self.timeZone();
        APIService.doRequest('timezones/' + item.ID(), 'PUT', ko.toJSON(item)).done(function () {
            self.timeZone(new TimeZone());
            allItems([]);
            self.getAllTimeZones();
        }).fail(function (jqXHR, textStatus, errorThrown) {
            
            handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
        });
    }

    self.addTimeZone = function (formElement) {
        self.error('');
        APIService.doRequest('timezones', 'POST', ko.toJSON(self.newTimeZone())).done(function (data) {
            allItems.push(new TimeZone(data.ID, data.Name, data.Owner, data.City, data.GMTOffset));
            self.newTimeZone(new TimeZone());
        }).fail(function (jqXHR, textStatus, errorThrown) {
            
            handleAPIFail(jqXHR.status, self, errorThrown, JSON.parse(jqXHR.responseText || "null"));
        });
    }
};
