<template>
  <div>
    <div v-if="zoneLoading" id="overlay">
      <div id="overlay-content">
        <div class="spinner-border" role="status"><span class="sr-only"></span></div>
        <div>Loading...</div>
      </div>
    </div>
    <div id="alerts" class="fixed-top">
      <div v-if="negativeAlert" id="negative-alert-popup" class="alert alert-danger" role="alert">{{ alertText }}</div>
      <div v-if="positiveAlert" id="positive-alert-popup" class="alert alert-success" role="alert">{{ alertText }}</div>
    </div>

    <Modal :showModal="showModal" :loading="loading" :hideFooter="addingRecord || editingRecord" @closeModal="handleCloseModal">
      <template #title>{{modalTitle}}</template>
      <Action v-if="addingRecord" @getZone="getZone" @showSuccess="showSuccess" @showError="showError"></Action>
      <EditRecord v-else-if="editingRecord" @getZone="getZone" @showSuccess="showSuccess" @showError="showError" :record="editRecord" :type="editType"></EditRecord>
      <p v-else>{{ deleteMessage }}</p>
    </Modal>

    <h1 class="mt-5">DNS Records</h1>
    <div class="container">
      <button class="btn btn-success mb-3" @click="addRecord">Add Record</button>
      <Domains @delete="handleDelete" @edit="handleEdit" :_data="zoneData.domains" :formatDateTime="formatDateTime"></Domains>
      <ARecords @delete="handleDelete" @edit="handleEdit" :_data="zoneData.aRecords" :formatDateTime="formatDateTime"></ARecords>
      <AAAARecords @delete="handleDelete" @edit="handleEdit" :_data="zoneData.aaaaRecords" :formatDateTime="formatDateTime"></AAAARecords>
      <CNAMERecords @delete="handleDelete" @edit="handleEdit" :_data="zoneData.cnameRecords" :formatDateTime="formatDateTime"></CNAMERecords>
      <MXRecords @delete="handleDelete" @edit="handleEdit" :_data="zoneData.mxRecords" :formatDateTime="formatDateTime"></MXRecords>
      <TXTRecords @delete="handleDelete" @edit="handleEdit" :_data="zoneData.txtRecords" :formatDateTime="formatDateTime"></TXTRecords>
    </div>

  </div>
</template>

<script>
import { ref, defineComponent, onMounted } from "vue";
import Modal from "./Modal.vue";
import Domains from "./tables/Domains.vue";
import ARecords from "./tables/ARecords.vue";
import AAAARecords from "./tables/AAAARecords.vue";
import CNAMERecords from "./tables/CNAMERecords.vue";
import MXRecords from "./tables/MXRecords.vue";
import TXTRecords from "./tables/TXTRecords.vue";
import Action from "./Action.vue";
import EditRecord from "./EditRecord.vue";
import axios from "axios";

export default defineComponent({
  components: {
    Modal,
    Domains,
    ARecords,
    AAAARecords,
    CNAMERecords,
    MXRecords,
    TXTRecords,
    Action,
    EditRecord,
  },
  name: "DNSRecords",
  setup() {
    const zoneData = ref({
        domains: [],
        aRecords: [],
        aaaaRecords: [],
        cnameRecords: [],
        mxRecords: [],
        txtRecords: [],
    });
    const token = ref("");
    const showModal = ref(false);
    const deleteName = ref("");
    const deleteType = ref("");
    const editRecord = ref({});
    const editType = ref("");
    const deleteMessage = ref("");
    const loading = ref(false);
    const zoneLoading = ref(false);
    const negativeAlert = ref(false);
    const positiveAlert = ref(false);
    const alertText = ref("");
    const modalTitle = ref("");
    const addingRecord = ref(false);
    const editingRecord = ref(false);

    const getZone = async () => {
      zoneLoading.value = true;

      try {
        const apiUrl = `${process.env.VUE_APP_API_ORIGIN}/api/zone`;
        const headers = {
          "Content-Type": "application/json",
          Accept: "application/json, */*;q=0.1",
        };

        const response = await axios.get(apiUrl, { headers });

        if (response.status === 200) {
          zoneData.value = response.data;
        }
      } catch (error) {
        showError("fetch", error);
        console.error("Error fetching data:", error);
      }
      zoneLoading.value = false;
    };

    const handleDelete = (name, type) => {
      addingRecord.value = false;
      editingRecord.value = false;
      modalTitle.value = "Delete Record";
      showModal.value = true;
      deleteName.value = name;
      deleteType.value = type;

      if (type === "domain") {
        deleteMessage.value =
          "Deleting a domain will delete all records associated with it. Are you sure you want to continue?";
      } else {
        deleteMessage.value = `Are you sure you want to delete this ${type} record?`;
      }
    };

    const handleEdit = (item, type) => {
      addingRecord.value = false;
      editingRecord.value = true;
      modalTitle.value = "Edit Record";
      showModal.value = true;
      editRecord.value = item;
      editType.value = type;
    };

    const handleCloseModal = async (value) => {
      if (value && deleteName.value && deleteType.value) {
        try {
          let apiUrl = "";
          if (deleteType.value === "domain") {
            apiUrl = `${process.env.VUE_APP_API_ORIGIN}/api/zone/${deleteName.value}`;
          } else {
            apiUrl = `${process.env.VUE_APP_API_ORIGIN}/api/zone/${deleteName.value}/${deleteType.value}`;
          }

          loading.value = true;
          const response = await axios.delete(apiUrl);

          if (response.status === 200) {
            showSuccess("delete");
            await getZone();
          }
        } catch (error) {
          loading.value = false;
          showError(`delete ${deleteType.value}`, error);
        }
      }
      loading.value = false;
      showModal.value = false;
      deleteName.value = "";
      deleteType.value = "";
      deleteMessage.value = "";
    };

    const formatDateTime = (dateTimeString) => {
      const options = {
        year: "numeric",
        month: "short",
        day: "numeric",
        hour: "numeric",
        minute: "numeric",
      };

      const dateTime = new Date(dateTimeString);
      return dateTime.toLocaleString(undefined, options);
    };

    const showSuccess = (action, editing=false) => {
      showModal.value = false;
      positiveAlert.value = true;
      if (action === "delete") {
        alertText.value = `Record deleted successfully.`;
      } else {
        alertText.value = `${action} record ${editing ? 'updated' : 'created'} successfully.`;
      }

      setTimeout(() => {
        alertText.value = "";
        positiveAlert.value = false;
      }, 3000);
    };

    const showError = (action, error, editing=false) => {
      showModal.value = false;
      negativeAlert.value = true;

      if (error.response?.data?.message) {
        alertText.value = "Error: " + error.response.data.message;
      } else {
        alertText.value = `There was an error ${editing ? 'updating' : 'creating'} the ${action} record.`;
      }

      setTimeout(() => {
        alertText.value = "";
        negativeAlert.value = false;
      }, 10000);
    };

    const addRecord = () => {
      modalTitle.value = "Add Record";
      showModal.value = true;
      editingRecord.value = false;
      addingRecord.value = true;
    };

    onMounted(getZone);

    return {
      zoneData,
      showModal,
      deleteName,
      deleteType,
      deleteMessage,
      editRecord,
      editType,
      loading,
      zoneLoading,
      negativeAlert,
      positiveAlert,
      alertText,
      modalTitle,
      addingRecord,
      editingRecord,
      handleDelete,
      handleEdit,
      handleCloseModal,
      formatDateTime,
      showSuccess,
      showError,
      getZone,
      addRecord,
    };
  },
});
</script>
<style scoped>
#overlay {
  position: fixed; /* Sit on top of the page content */
  width: 100%; /* Full width (cover the whole page) */
  height: 100%; /* Full height (cover the whole page) */
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background-color: rgba(0,0,0,0.5); /* Black background with opacity */
  z-index: 2; /* Specify a stack order in case you're using a different order for other elements */
}

#overlay-content {
  color: white;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  z-index: 3;
}
</style>
