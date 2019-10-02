var app = angular.module("app", ['ui.router', 'ui.bootstrap']);

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
            url: '/Order',
            templateUrl: 'App/View/Order/ListOrder.html'
        })
        .state('shopping', {
            url: '/Order',
            templateUrl: 'App/View/Shopping.html'
        })
        .state('orderform', {
            url: '/Order/Form',
            templateUrl: 'App/View/Order/OrderForm.html'
        })



});
app.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);
}]);
