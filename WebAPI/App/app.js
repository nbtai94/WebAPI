var app = angular.module("app", ['ui.router', 'ui.bootstrap',"kendo.directives"]);
app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');

    $stateProvider

        .state('home', {
            url: '/Home',  //Tên hiển thị trên URL
            templateUrl: 'home/index'  //Đường dẫn file
        })
        .state('list', {
            url: '/ListProduct',
            templateUrl: 'App/View/Product/ListProduct.html'
        })
        .state('contact', {
            url: '/Contact',
            templateUrl: 'home/contact'
        })
        .state('customer', {
            url: '/ListCustomer',
            templateUrl: 'App/View/Customer/ListCustomer.html'
        })

        .state('form', {
            url: '/Form?id',
            templateUrl: 'App/View/Product/ProductForm.html'
        })
        .state('cusForm', {
            url: '/CusForm?id',
            templateUrl: 'App/View/Customer/CustomerForm.html'
        })
        .state('order', {
            url: '/ListOrder',
            templateUrl: 'App/View/Order/ListOrder.html'
        })
      
        .state('shopping', {
            url: '/Shopping',
            templateUrl: 'App/View/Shopping.html'
        })
        .state('orderform', {
            url: '/Order?id',
            templateUrl: 'App/View/Order/OrderForm.html'
        }) 
        .state('orderdetail', {
            url: '/OrderInfo?id',
            templateUrl: 'App/View/Order/OrderDetail.html'
        })
});
app.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);
}]);