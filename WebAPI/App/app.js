var app = angular.module("app", ['ui.router']);

app.config(function ($stateProvider, $urlRouterProvider) {

    $urlRouterProvider.otherwise('/home');

    $stateProvider

        .state('home', {
            url: '/Home',  //Tên hiển thị trên URL
            templateUrl: 'home/index'  //Đường dẫn file
        })
        .state('list', {
            url: '/ListProduct',
            templateUrl:'App/Template/ListProduct.html'      
        })
        .state('contact', {
            url: '/Contact',
            templateUrl: 'home/contact'
        })
        .state('form', {
            url: '/Form?id',
            templateUrl: 'App/Template/ProductForm.html'
        })
     
   
})
