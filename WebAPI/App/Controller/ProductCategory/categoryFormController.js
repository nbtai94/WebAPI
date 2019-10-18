app.controller('categoryFormController', ['$scope', '$stateParams', '$http', function ($scope, $stateParams, $http, $state,$window) {
    var vm = this;
    vm.back = back;
    vm.save = save;
    vm.category = {};
    vm.id = $stateParams.id;

    function back() {
        history.back();

    }
    function save() {
        if (!vm.id) {
            debugger;
            $http({
                method: "POST",
                url: "api/ProductCategoriesAPI/ProductCategories",
                datatype: "json",
                data: angular.toJson(vm.category)
            }).then(function (response) {
                toastr["success"]("Thêm thành công!");

                $state.go("categories", {});

            }, function errorCallback(res) {
                    toastr["error"]("Vui lòng điền đủ thông tin và thử lại!")

            });
        }
        else {
         
            $http({
                method: "Put",
                url: "api/ProductCategoriesAPI/ProductCategory",
                data: angular.toJson(vm.category)
            }).then(function (res) {
                toastr["success"]("Chỉnh sửa thành công!")

            }, function errorCallback(res) {
                toastr["error"]("Lỗi rồi bạn ơi thử lại đi!")
            })
        }
    }
   
    
    if (vm.id) {
        $http({
            method: "GET",
            url: "api/ProductCategoriesAPI/ProductCategory?Id=" + vm.id
        }).then(function successCallback(res) {
            vm.category = res.data.data;
        }, function errorCallback(res) {
            toastr["error"]("Lỗi rồi bạn ơi thử lại đi!")
        })
    }






}]);