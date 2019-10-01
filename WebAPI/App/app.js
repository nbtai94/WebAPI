var app = angular.module("app", ['ui.router', 'ui.bootstrap','ngAnimate']);

app.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/home');

    $stateProvider

        .state('home', {
            url: '/Home',  //Tên hiển thị trên URL
            templateUrl: 'home/index'  //Đường dẫn file
        })
        .state('list', {
            url: '/ListProduct',
            templateUrl: 'App/Template/ListProduct.html'
        })
        .state('contact', {
            url: '/Contact',
            templateUrl: 'home/contact'
        })
        .state('customer', {
            url: '/ListCustomer',
            templateUrl: 'App/Template/ListCustomer.html'
        })

        .state('form', {
            url: '/Form?id',
            templateUrl: 'App/Template/ProductForm.html'
        })
        .state('cusForm', {
            url: '/CusForm?id',
            templateUrl: 'App/Template/CustomerForm.html'
        })
        .state('order', {
            url: '/Order',
            templateUrl:'App/Template/ListOrder.html'
        })
        .state('shopping', {
            url: '/Order',
            templateUrl: 'App/Template/Shopping.html'
        })
        .state('orderform', {
            url: '/Order/Form',
            templateUrl: 'App/Template/OrderForm.html'
        })

        
   
})
app.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);
}]);
