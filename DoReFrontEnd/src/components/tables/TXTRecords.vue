<template>
  <div>
    <div class="heading-container">
      <h2>TXT Records <label for="TXT Records"> 
        <InfoIcon>A TXT record lets administrators enter text into the DNS system. It can be used to prevent email spam and prove domain ownership. </InfoIcon>
      </label></h2>
    </div>
    <TableComponent
      class="mb-5"
      @delete="deleteRecord"
      @edit="editRecord"
      :fields="fields"
      :tableData="_data"
      sortBy="created"
    />
  </div>
</template>
  
<script>
import { defineComponent } from "vue";
import TableComponent from "../Table.vue";
import InfoIcon from "../InfoIcon.vue";

export default defineComponent({
  name: "TXTRecords",
  components: {
    TableComponent,
    InfoIcon
  },
  props: {
    _data: {},
    formatDateTime: Function,
  },
  emits: ["delete", "edit"],
  setup(props, { emit }) {
    const fields = [
      {
        key: "name",
        sortable: true,
        label: "Subdomain Name",
      },
      {
        key: "values",
        sortable: false,
        label: "Values",
        formatter: (value) => {
          return value.join(" | ");
        },
      },
      {
        key: "created",
        sortable: true,
        label: "Created",
        formatter: props.formatDateTime,
      },
      {
        key: "updated",
        sortable: true,
        label: "Updated",
        formatter: props.formatDateTime,
      },
      {
        key: "ttl",
        sortable: true,
        label: "TTL",
      },
    ];

    const deleteRecord = async (record) => {
      emit("delete", record.name, "TXT");
    };

    const editRecord = async (record) => {
      emit("edit", record, "TXT");
    };

    return {
      fields,
      deleteRecord,
      editRecord,
    };
  },
});
</script>
  