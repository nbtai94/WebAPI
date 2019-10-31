(function () {
    'use strict';

    app.controller('productFormController', controller);

    controller.$inject = ['$scope', '$stateParams', '$http'];

    function controller($scope, $stateParams, $http) {
        var vm = this;
        vm.back = back;
        vm.save = save;
        vm.product = {};
        vm.categories = {};
        vm.id = $stateParams.id;

        function back() {
            history.back();
        }
        $http({
            method: "GET",
            url: "/odata/ProductCategories",
        }).then(function successCallback(res) {
            vm.categories = res.data.value;
        }, function errorCallback(res) {
            toastr["error"]("Lỗi rồi , ko tải được dữ liệu!");
        })


        function save() {
            if (vm.id) {
                $http({
                    method: "Put",
                    url: "/odata/Products" + "(" + vm.id + ")",
                    data: angular.toJson(vm.product)
                }).then(function (res) {
                    toastr["success"]("Chỉnh sửa thành công!");
                    vm.back();
                });
            }
            else {
                $http({
                    method: "POST",
                    //url: "api/ProductsAPI/Products",
                    url: "/odata/Products",
                    datatype: "json",
                    data: JSON.stringify(vm.product)
                }).then(function (response) {
                    toastr["success"]("Thêm thành công!"),
                        vm.back();
                })
            }
        }
        if (vm.id) {
            $http({
                method: "GET",
                //url: "api/ProductCategoriesAPI/ProductCategory?Id=" + vm.id
                url: "/odata/Products" + "(" + vm.id + ")",
                datatype: "odata"
            }).then(function successCallback(res) {
                vm.product = res.data;
            }, function errorCallback(res) {
                toastr["error"]("Lỗi rồi bạn ơi thử lại đi!")
            })
        }
        vm.price = {
            format: "0,",
            step: 1000
        }
    }
})();
