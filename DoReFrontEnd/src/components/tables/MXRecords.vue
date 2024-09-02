<template>
  <div>
    <div class="heading-container">
      <h2>MX Records <label for="MX Records"> 
        <InfoIcon>An MX records redirects email to a mail server.</InfoIcon>
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
  name: "MXRecords",
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
          let formattedArray = value.map((v) => {
            return `${v.exchange} (Preference ${v.preference})`;
          });
          return formattedArray.join(" | ");
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
      emit("delete", record.name, "MX");
    };

    const editRecord = async (record) => {
      emit("edit", record, "MX");
    };

    return {
      fields,
      deleteRecord,
      editRecord,
    };
  },
});
</script>
  