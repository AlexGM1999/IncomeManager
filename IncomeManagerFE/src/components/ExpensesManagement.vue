<template>
    <div class="q-pa-md">

        <div class="q-gutter-y-md" style="max-width: 2000px">
            <q-card>
                <q-tabs v-model="tab"
                        dense
                        class="text-grey"
                        active-color="primary"
                        indicator-color="primary"
                        align="justify"
                        narrow-indicator>
                    <q-tab name="main" label="Main" />
                    <q-tab name="statistics" label="Statistics" />
                    <q-tab name="goals" label="Goals" />
                </q-tabs>

                <q-tab-panels v-model="tab" animated>
                    <q-tab-panel name="main">

                        <div class="q-pa-md">
                            <q-btn color="secondary" label="Add new" @click="persistent = true" />

                            <q-dialog v-model="persistent" persistent transition-show="scale" transition-hide="scale">
                                <q-card class="bg-teal text-white" style="width: 300px">
                                    <q-card-section>
                                        <div class="text-h6">Add new expense</div>
                                    </q-card-section>

                                    <q-card-section class="q-pt-none">
                                        <q-input color="white" rounded outlined v-model="type" label="Type" />
                                        <q-input color="white" rounded outlined v-model="amount" label="Amount" />
                                        <q-input color="white" rounded outlined v-model="investment" label="Investment" />
                                    </q-card-section>

                                    <q-card-actions align="right" class="bg-white text-teal">
                                        <q-btn flat label="Add" @click="addExpense" v-close-popup />
                                        <q-btn flat label="Cancel" v-close-popup />
                                    </q-card-actions>
                                </q-card>
                            </q-dialog>

                            <q-table title="Expenses"
                                     :rows=rows
                                     :columns=columns
                                     row-key="type">
                                <q-btn color="secondary" label="Add new" />
                                <template v-slot:body="props">
                                    <q-tr :props="props">
                                        <q-td key="type" :props="props">
                                            {{ props.row.type }}
                                        </q-td>
                                        <q-td key="amount" :props="props">
                                            {{ props.row.amount }}
                                        </q-td>
                                        <q-td key="dateTime" :props="props">
                                            {{ props.row.dateTime }}
                                        </q-td>
                                        <q-td key="investment" :props="props">
                                            {{ props.row.investment }}
                                        </q-td>
                                        <q-td key="action" :props="props">
                                            <q-btn icon="delete" @click.stop="deleteClick(props.row)" dense flat />
                                            <q-btn icon="edit" @click.stop="editClick(props.row)" dense flat />
                                        </q-td>
                                    </q-tr>
                                </template>
                            </q-table>
                        </div>

                        <div class="q-pa-sm q-gutter-sm">
                            <q-dialog v-model="show_dialog">
                                <q-card>
                                    <q-card-section>
                                        <div class="text-h6">Update expense</div>
                                    </q-card-section>

                                    <q-card-section>
                                        <div class="row">
                                            <q-input v-model="editedItem.Type" label="Type"></q-input>
                                            <q-input v-model="editedItem.Amount" label="Amount"></q-input>
                                            <q-input v-model="editedItem.Investment" label="Investment"></q-input>
                                        </div>
                                    </q-card-section>

                                    <q-card-actions align="right">
                                        <q-btn flat label="Update" color="primary" v-close-popup @click="updateRow"></q-btn>
                                    </q-card-actions>
                                </q-card>
                            </q-dialog>
                        </div>

                    </q-tab-panel>

                    <q-tab-panel name="statistics">
                        <div class="text-h6">Alarms</div>
                        Lorem ipsum dolor sit amet consectetur adipisicing elit.
                    </q-tab-panel>

                    <q-tab-panel name="goals">
                        <div class="text-h6">Movies</div>
                        Lorem ipsum dolor sit amet consectetur adipisicing elit.
                    </q-tab-panel>
                </q-tab-panels>
            </q-card>
        </div>
    </div>
</template>

<script>
    import { ref } from 'vue'
    export default {
        setup() {
            return {
                persistent: ref(false),
                type: ref(''),
                amount: ref(''),
                investment: ref(''),
                ph: ref(''),
                dense: ref(false),
                tab: ref('main'),
            }
        },
        name: 'Expenses-component',
        data() {
            return {
                columns: [
                    {
                        name: 'type',
                        required: true,
                        label: 'Type',
                        align: 'left',
                        field: row => row.name,
                        format: val => `${val}`,
                        sortable: true
                    },
                    { name: 'amount', align: 'center', label: 'Amount', field: row => row.amount, sortable: true },
                    { name: 'dateTime', align: 'center', label: 'Date', field: row => row.dateTime, sortable: true },
                    { name: 'investment', align: 'center', label: 'Investment', field: 'investment', sortable: true },
                    { name: 'action', required: true, label: 'Action', field: 'action', sortable: false },

                ],

                rows: [
                    {
                    }
                ],
                show_dialog: false,
                editedItem: '',
            }
        },

        methods: {
            async deleteClick(row) {
                await this.$http.delete('http://localhost:55131/api/Expenses/' + row.id)

                this.$http.get('http://localhost:55131/api/Expenses')
                    .then(response => {
                        console.log(response.data)
                        this.rows = response.data
                    })
            },
            editClick(item) {
                this.editedItem = item;
                this.show_dialog = true;
                this.editedItem.Type = item.type;
                this.editedItem.Amount = item.amount;
                this.editedItem.Investment = item.investment;
            },
            async updateRow() {
                await this.$http.put(
                    'http://localhost:55131/api/Expenses/'
                    + this.editedItem.id,
                    {
                        id: this.editedItem.id,
                        type: this.editedItem.Type,
                        amount: this.editedItem.Amount,
                        investment: this.editedItem.Investment,
                        userId: 1
                    }
                )
                .then(response => {
                    console.log(response.data)
                    this.rows.push(response.data)
                })

                this.$http.get('http://localhost:55131/api/Expenses')
                    .then(response => {
                        console.log(response.data)
                        this.rows = response.data
                    })

            },
            addExpense() {
                this.$http.post('http://localhost:55131/api/Expenses', { type: this.type, amount: this.amount, investment: null, userId: 1 })
                    .then(response => {
                        console.log(response.data)
                        this.rows.push(response.data)
                    })
            }
        },

        mounted() {
            this.$http.get('http://localhost:55131/api/Expenses')
                .then(response => {
                    console.log(response.data)
                    this.rows = response.data
                })
        }
    }
</script>
