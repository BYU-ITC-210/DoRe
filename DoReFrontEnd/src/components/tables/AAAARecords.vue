<template>
  <div>
    <div class="heading-container">
      <h2>AAAA Records <label for="AAAA Records"> 
        <InfoIcon>AAAA Records match a domain name to an IPv6 address, like what an A record does for an IPv4 address.</InfoIcon>
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
  name: "AAAARecords",
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
        label: "IPv6 Address(s)",
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
      emit("delete", record.name, "AAAA");
    };

    const editRecord = async (record) => {
      emit("edit", record, "AAAA");
    };

    return {
      fields,
      deleteRecord,
      editRecord,
    };
  },
});
</script>
