import * as Vue from 'vue'
import * as VueRouter from 'vue-router'
import App from './App.vue'

import "quasar/dist/quasar.sass"

import Salary from './components/SalaryManagement.vue'
import Home from './components/HomeManagement.vue'
import Expenses from './components/ExpensesManagement.vue'
import Income from './components/IncomeManagement.vue'
import Investments from './components/InvestmentsManagement.vue'
import { Quasar } from 'quasar'
import quasarUserOptions from './quasar-user-options'
import axios from 'axios'
import VueAxios from 'vue-axios'

const routes = [{
    path: '/Salary',
    component: Salary
},
{
    path: '/',
    component: Home
},
{
    path: '/Expenses',
    component: Expenses
},
{
    path: '/Income',
    component: Income
},
{
    path: '/Investments',
    component: Investments
}
]

const router = new VueRouter.createRouter({
    history: VueRouter.createWebHistory(),
    routes
})

const app = Vue.createApp(App)

app.use(VueAxios, axios)
app.use(Quasar, quasarUserOptions).use(router).mount('#app');