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
                </q-tabs>

                <q-tab-panels v-model="tab" animated>
                    <q-tab-panel name="main">

                        <div class="q-pa-md">
                            <q-btn color="secondary" label="Add new" @click="persistent = true" />

                            <q-dialog v-model="persistent" persistent transition-show="scale" transition-hide="scale">
                                <q-card class="bg-teal text-white" style="width: 300px">
                                    <q-card-section>
                                        <div class="text-h6">Add new investment</div>
                                    </q-card-section>

                                    <q-card-section class="q-pt-none">
                                        <q-select color="white" rounded outlined v-model="source" :options="options" label="Source" :rules="[val => val !== null && val !== '' || 'Please choose the source']"/>
                                        <q-input color="white" rounded outlined v-model="amount" type="number" label="Amount" :rules="[val => val !== null && val !== '' || 'Please type the amount']" />

                                    </q-card-section>

                                    <q-card-actions align="right" class="bg-white text-teal">
                                        <q-btn flat label="Add" @click="addInvestment" v-close-popup />
                                        <q-btn flat label="Cancel" v-close-popup />
                                    </q-card-actions>
                                </q-card>
                            </q-dialog>

                            <q-table title="Investments"
                                     :rows=rows
                                     :columns=columns
                                     row-key="type">
                                <q-btn color="secondary" label="Add new" />
                                <template v-slot:body="props">
                                    <q-tr :props="props">
                                        <q-td key="source" :props="props">
                                            {{ props.row.source }}
                                        </q-td>
                                        <q-td key="amount" :props="props">
                                            {{ props.row.amount }}
                                        </q-td>
                                        <q-td key="dateTime" :props="props">
                                            {{ props.row.dateTime }}
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
                                        <div class="text-h6">Update investment</div>
                                    </q-card-section>

                                    <q-card-section>
                                        <div class="row">
                                            <q-select v-model="editedItem.Source" :options="options" label="Source" :rules="[val => val !== null && val !== '' || 'Please choose the source']" />
                                            <q-input v-model="editedItem.Amount" type="number" label="Amount" :rules="[val => val !== null && val !== '' || 'Please type the amount']"></q-input>
                                        </div>
                                    </q-card-section>

                                    <q-card-actions align="right">
                                        <q-btn flat label="Update" color="primary" v-close-popup @click="updateRow"></q-btn>
                                    </q-card-actions>
                                </q-card>
                            </q-dialog>

                            <q-dialog v-model="show_dialogDelete">
                                <q-card>
                                    <q-card-section>
                                        <div class="text-h6">Delete Investment</div>
                                    </q-card-section>

                                    <q-card-section>
                                        <div class="row">
                                            <p>Are you sure you want to delete the investment permanently?</p>
                                        </div>
                                    </q-card-section>

                                    <q-card-actions align="right">
                                        <q-btn flat label="Delete" color="primary" v-close-popup @click="deleteInvestment"></q-btn>
                                        <q-btn flat label="Cancel" color="primary" v-close-popup></q-btn>
                                    </q-card-actions>
                                </q-card>
                            </q-dialog>

                        </div>
                    </q-tab-panel>

                    <q-tab-panel name="statistics">
                        <q-btn id="loadBtn" style="width: 100%" @click="getData">Load Stats</q-btn>
                        <div id="chartsTitle" style="display: none" class="text-h6">Charts</div>
                        <div style="margin: 0 auto; width:40%">
                            <canvas id="myChart"></canvas>
                        </div>
                    </q-tab-panel>
                </q-tab-panels>
            </q-card>
        </div>
    </div>
</template>

<script>
    import { ref } from 'vue'
    import Chart from 'chart.js/auto';
    import _ from 'lodash';

    export default {
        setup() {
            return {
                persistent: ref(false),
                amount: ref(''),

                ph: ref(''),
                dense: ref(false),
                tab: ref('main'),

                source: ref(''),
                options: [
                    'Personal', 'Bank', 'Investors', 'Other'
                ]
            }
        },
        name: 'Investments-component',
        data() {
            return {
                columns: [
                    {
                        name: 'source',
                        required: true,
                        label: 'Source',
                        align: 'left',
                        field: row => row.source,
                        format: val => `${val}`,
                        sortable: true
                    },
                    { name: 'amount', align: 'center', label: 'Amount', field: row => row.amount, sortable: true },
                    { name: 'dateTime', align: 'center', label: 'Date', field: row => row.dateTime, sortable: true },
                    { name: 'action', required: true, label: 'Action', field: 'action', sortable: false },

                ],

                rows: [
                    {
                    }
                ],
                show_dialog: false,
                show_dialogDelete:false,
                editedItem: '',
                deletedItem:''
            }
        },

        methods: {
             deleteClick(item) {
                this.show_dialogDelete = true;
                this.deletedItem = item;
            },
            editClick(item) {
                this.editedItem = item;
                this.show_dialog = true;
                this.editedItem.Source = item.source;
                this.editedItem.Amount = item.amount;
            },
            async deleteInvestment() {
                await this.$http.delete('http://localhost:55131/api/Investments/' + this.deletedItem.id)

                this.getInvestments()
            },
            async updateRow() {
                await this.$http.put(
                    'http://localhost:55131/api/Investments/'
                    + this.editedItem.id,
                    {
                        id: this.editedItem.id,
                        source: this.editedItem.Source,
                        amount: this.editedItem.Amount,
                        userId: 1
                    }
                )
                    .then(response => {
                        this.rows.push(response.data)
                    })

                this.getInvestments()
            },
            addInvestment() {
                this.$http.post('http://localhost:55131/api/Investments', { source: this.source, amount: this.amount, userId: 1 })
                    .then(response => {
                        this.rows.push(response.data)
                    }),
                    this.ResetText()
            },
            ResetText() {
                this.source = '',
                this.amount = ''
            },
            getInvestments() {
                this.$http.get('http://localhost:55131/api/Investments')
                    .then(response => {
                        this.rows = response.data
                    })
            },
            getData() {
                this.showStatistics()
            },
            showStatistics() {
                this.chart = null;
                let chartsTitle = document.getElementById('chartsTitle')
                chartsTitle.style.display = 'block'
                let load_btn = document.getElementById('loadBtn')
                load_btn.style.display = 'none';
                var canvas = document.getElementById('myChart')
                let ctx = canvas.getContext('2d')
                let data = ''
                this.$http.get('http://localhost:55131/api/Investments')
                    .then(response => {
                        data = _.mapValues(_.groupBy(response.data, 'source'), value => _.sumBy(value, 'amount'))
                        let backgroundColors = []
                        for (let i = 0; i < Object.keys(data).length; i++) {
                            backgroundColors.push(
                                `rgb(${Math.random() * 256}, ${Math.random() * 256}, ${Math.random() * 256})`
                            )
                        }
                        let dataItem = {
                            labels: Object.keys(data),
                            datasets: [{
                                data: Object.values(data),
                                backgroundColor: backgroundColors,
                                hoverOffset: 4
                            }]
                        }
                        this.chart = new Chart(ctx, {
                            type: 'pie',
                            data: dataItem,
                        });
                    })
            }
        },

        mounted() {
            this.$http.get('http://localhost:55131/api/Investments')
                .then(response => {
                    this.rows = response.data
                })
        }
    }
</script>
