<template>
  <div>
    <div class="heading-container">
      <h2>Domains <label for="Domains"> 
        <InfoIcon>This is the domain name record. For example, https://learningsuite.byu.edu, learningsuite is the domain name.  </InfoIcon>
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
  name: "Domains",
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
        key: "context",
        sortable: true,
        label: "Context",
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
    ];

    const deleteRecord = async (domain) => {
      emit("delete", domain.name, "domain");
    };

    const editRecord = async (record) => {
      emit("edit", record, "domain");
    };

    return {
      fields,
      deleteRecord,
      editRecord,
    };
  },
});
</script>
