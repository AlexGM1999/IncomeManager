<template>
    <div id="app">
        <q-toolbar class="q-pa-md q-gutter-lg">
            <q-btn align="between" class="btn-fixed-width" color="secondary" icon="home" @click="gotoHome()" />
            <q-btn flat round dense label="Salary" icon="price_change" @click="gotoSalary()" />
            <q-btn flat round dense label="Investments" icon="timeline" @click="gotoInvestments()" />
            <q-btn flat round dense label="Income" icon="euro" @click="gotoIncome()" />
            <q-btn flat round dense label="Expenses" icon="shopping_cart_checkout" @click="gotoExpenses()" />

            <q-space />

            <q-fab v-model="fab2"
                   label="Funds"
                   vertical-actions-align="right"
                   color="secondary"
                   icon="keyboard_arrow_down"
                   direction="down"
                   @click="getFunds">
                <q-fab-action color="primary" :label=personal />
                <q-fab-action color="secondary" :label="bank" />
                <q-fab-action color="orange" :label="investors" />
                <q-fab-action color="accent" :label="other" />
            </q-fab>
        </q-toolbar>

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
            },
            personal: 'Personal Balance: NA',
            bank: 'Bank Balance: NA',
            investors: 'Investors Balance: NA',
            other: 'Other Balance: NA'

        }
        },
    methods: {
        getFunds() {
            this.$http.get('http://localhost:55131/api/Users')
                .then(response => {
                    this.personal = `Personal Balance: ${response.data[0].personalBalance}`,
                    this.bank = `Bank Balance: ${response.data[0].bankBalance}`,
                    this.investors = `Investors Balance: ${response.data[0].investorsBalance}`
                    this.other = `Other Balance: ${response.data[0].otherBalance}`
                })
        }
        }
}
</script>
