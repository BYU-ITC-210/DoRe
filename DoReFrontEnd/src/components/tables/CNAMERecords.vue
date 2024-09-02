<template>
  <div>
    <div class="heading-container">
      <h2>CNAME Records <label for="CNAME Records"> 
        <InfoIcon>A CNAME record points to an alias for a different domain. For example, you could have a CNAME record for learningsuite.com that points to home.learningsuite.com. </InfoIcon>
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
  name: "CNAMERecords",
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
        key: "cname",
        sortable: false,
        label: "CNAME",
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
      emit("delete", record.name, "CNAME");
    };

    const editRecord = async (record) => {
      emit("edit", record, "CNAME");
    };

    return {
      fields,
      deleteRecord,
      editRecord,
    };
  },
});
</script>
  