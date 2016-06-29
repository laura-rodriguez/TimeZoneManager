var MasterViewModel = function () {
   var self = this;
    
   self.LoginViewModel = new LoginViewModel();
   self.TimeZoneViewModel = new TimeZonesViewModel();
   self.UserViewModel = new UserViewModel();
}