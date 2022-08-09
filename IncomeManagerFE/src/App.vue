<template>
    <div id="app">
        <div class="q-pa-md q-gutter-lg">
               <q-btn align="between" class="btn-fixed-width" color="secondary"  icon="home" @click="gotoHome()" />
               <q-btn flat round dense label="Salary" icon="price_change" @click="gotoSalary()" />
               <q-btn flat round dense label="Investments" icon="timeline" @click="gotoInvestments()" />
               <q-btn flat round dense label="Income" icon="euro" @click="gotoIncome()" />
               <q-btn flat round dense label="Expenses" icon="shopping_cart_checkout" @click="gotoExpenses()" />
        </div>
        <router-view />
    </div>
</template>

<script>
export default {
    name: 'app',
    data () {
        return {
            gotoExpenses() {
                window.location.href = '/Expenses'
            },
            gotoSalary() {
                window.location.href = '/Salary'
            },
            gotoInvestments() {
                window.location.href = '/Investments'
            },
            gotoIncome() {
                window.location.href = '/Income'
            },
            gotoHome() {
                window.location.href = '/'
            }
        }
    }
}
</script>
