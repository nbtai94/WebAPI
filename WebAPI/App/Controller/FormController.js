app.controller('FormController', ['$scope', '$stateParams', '$http', function ($scope, $stateParams, $http) {
    var vm = this;
    vm.back = back;
    vm.id = $stateParams.id;

    function back() {
        history.back();
    }





}])