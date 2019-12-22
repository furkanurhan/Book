<template>
  <div class="layout-padding">
    <v-data-table
      :headers="headers"
      :items="bookList"
      :items-per-page="15"
      :search="search"
      class="elevation-1"
      style="overflow: hidden"
    >
      <template v-slot:top>
        <v-toolbar
          flat
          color="white"
        >
          <v-toolbar-title>KİTAP LİSTESİ</v-toolbar-title>
          <v-divider
            class="mx-4"
            inset
            vertical
          />
          <v-spacer />
          <v-text-field
            v-model="search"
            append-icon="search"
            label="Ara"
            single-line
            hide-details
          />
          <v-spacer />
          <v-dialog
            v-model="dialog"
            max-width="800px"
          >
            <template v-slot:activator="{ on }">
              <v-btn
                color="primary"
                dark
                class="mb-2"
                v-on="on"
              >
                YENİ
              </v-btn>
            </template>
            <v-card height="100%">
              <v-card-title>
                <span class="headline">{{ formTitle }}</span>
              </v-card-title>
              <v-card-text>
                <v-container style="max-width:100%;padding-top:0em;padding-bottom:0em">
                  <v-row>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                      style="padding-bottom: 0px;padding-top:0px"
                    >
                      <v-text-field
                        v-model="editedItem.title"
                        label="Kitap Adı"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                      style="padding-bottom: 0px;padding-top:0px"
                    >
                      <v-text-field
                        v-model="editedItem.price"
                        append-icon="fas fa-lira-sign"
                        label="Fiyat"
                        @input="checkIfValid()"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                      style="padding-bottom: 0px;padding-top:0px"
                    >
                      <v-text-field
                        v-model="editedItem.publishedDate"
                        v-mask="{ mask: 'NN/NN/NNNN', tokens: tokens }"
                        label="Basım Tarihi"
                        hint="Gün/Ay/Yıl"
                      />
                    </v-col>
                    <v-col
                      cols="12"
                      sm="6"
                      md="4"
                      style="padding-bottom: 0px;padding-top:0px"
                    >
                      <v-text-field
                        v-model="editedItem.authorName"
                        label="Kitap Yazarı"
                      />
                    </v-col>
                  </v-row>
                </v-container>
              </v-card-text>
              <v-card-actions>
                <v-spacer />
                <v-btn
                  color="blue darken-1"
                  text
                  @click="close()"
                >
                  İPTAL
                </v-btn>
                <v-btn
                  color="blue darken-1"
                  text
                  @click="save()"
                >
                  KAYDET
                </v-btn>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>
      <template v-slot:item.action="{ item }">
        <v-icon
          small
          class="mr-4"
          @click="saveItem(item)"
        >
          fa-edit
        </v-icon>
        <v-icon
          small
          class="mr-4"
          @click="deleteItem(item)"
        >
          fas fa-trash-alt
        </v-icon>
      </template>
    </v-data-table>
  </div>
</template>

<script>
import axios from 'axios'
import { mask } from 'vue-the-mask'
import moment from 'moment'
const DEFAULT_DATETIME_FORMAT = 'DD/MM/YYYY'
export default {
  components: {
  },
  directives: { mask },
  data () {
    return {
      search: '',
      tokens: {
        '#': { pattern: /\d/ },
        'X': { pattern: /[0-9a-zA-Z]/ },
        'S': { pattern: /[a-zA-Z]/ },
        'N': { pattern: /[0-9]/ },
        'A': { pattern: /[0-9a-zA-Z]/, transform: v => v.toLocaleUpperCase() },
        'D': { pattern: /[a-zA-Z]/, transform: v => v.toLocaleUpperCase() },
        'a': { pattern: /[a-zA-Z]/, transform: v => v.toLocaleLowerCase() },
        '!': { escape: true }
      },
      bookList1: [],
      // loading: false,
      headers: [
        {
          text: 'Kitap Adı',
          align: 'left',
          sortable: false,
          value: 'title'
        },
        { text: 'Fiyat', value: 'price' },
        { text: 'Basım Tarihi', value: 'publishedDate' },
        { text: 'Kitap Yazarı', value: 'authorName' },
        { text: 'İşlemler', value: 'action', sortable: false }
      ],
      dialog: false,
      editedIndex: -1,
      editedItem: {
        id: null,
        title: '',
        price: null,
        publishedDate: null,
        // authorId: null,
        authorName: ''
      },
      defaultItem: {
        id: null,
        title: '',
        price: null,
        publishedDate: null,
        // authorId: null,
        authorName: ''
      }
    }
  },

  computed: {
    formTitle () {
      return this.editedIndex === -1 ? 'YENİ KİTAP' : 'GÜNCELLE'
    },
    bookList () {
      return this.bookList1.map((elm) => {
        const el = {
          ...elm
        }
        el.id = elm.id
        el.title = elm.title
        el.price = elm.price
        el.publishedDate = this.formatDate(elm.publishedDate)
        el.authorName = elm.authorName
        return el
      })
    },
    PublishedDateUTC () {
      debugger
      const [day, month, year] = this.editedItem.publishedDate.split('/')
      debugger
      return new Date(year, month, day)
    }

  },
  watch: {
    dialog (val) {
      val || this.close()
    }
  },
  created () {
    this.initialize()
  },
  methods: {
    saveItem (item) {
      this.editedIndex = this.bookList.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },
    deleteItem (item) {
      axios.post('/api/books/delete', { ID: item.id })
        .then((response) => {
        })
        .catch((e) => {
          console.log(e)
        })
      this.initialize()
    },
    close () {
      this.dialog = false
      setTimeout(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      }, 300)
    },
    save () {
      if (this.editedIndex > -1) {
        axios.post('/api/books/update', { ID: this.editedItem.id, Title: this.editedItem.title, Price: this.editedItem.price, publishedDate: this.PublishedDateUTC, AuthorName: this.editedItem.authorName })
          .then((response) => {
          })
          .catch((e) => {
            console.log(e)
          })
        this.initialize()
      } else {
        axios.post('/api/books/insert', { Title: this.editedItem.title, Price: this.editedItem.price, publishedDate: this.PublishedDateUTC, AuthorName: this.editedItem.authorName })
          .then((response) => {
          })
          .catch((e) => {
            console.log(e)
          })
        this.initialize()
      }
      this.close()
    },
    checkIfValid () {
      console.log(this.editedItem.price)
      this.editedItem.price = this.editedItem.price.replace(/,/g, '.')
    },
    formatDate (a) {
      return a ? moment(a).format(DEFAULT_DATETIME_FORMAT) : ''
    },
    initialize () {
      axios.get('/api/books')
        .then((response) => {
          debugger
          this.bookList1 = response.data.data
        })
        .catch((e) => {
          console.log(e)
        })
    }
  }
}
</script>

<style scoped>
  /* .v-dialog__content >>> .v-dialog {
    height: 90%
  }
  */
</style>
