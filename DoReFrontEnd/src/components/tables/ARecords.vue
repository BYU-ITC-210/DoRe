<template>
  <div>
    <div class="heading-container">
      <h2>A Records <label for="A Records"> 
        <InfoIcon>An A record indicates the IP address of the domain. A records hold IPv4 addresses. A stands for address in this case.</InfoIcon>
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
  name: "ARecords",
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
        label: "IPv4 Address(s)",
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
      emit("delete", record.name, "A");
    };

    const editRecord = async (record) => {
      emit("edit", record, "A");
    };

    return {
      fields,
      deleteRecord,
      editRecord,
    };
  },
});
</script>
