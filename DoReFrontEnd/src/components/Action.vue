<template>
  <div class="container">
    <form @submit.prevent="submitForm">
      <div class="mb-3">
        <label for="action">
          Action:
          <InfoIcon>Select the type of DNS record action to perform.</InfoIcon>
        </label>
        <select class="form-select" id="action" v-model="selectedAction" @change="resetForm">
          <option value="Claim Domain">Claim Domain</option>
          <option value="A">Create A Record</option>
          <option value="AAAA">Create AAAA Record</option>
          <option value="CNAME">Create CNAME Record</option>
          <option value="MX">Create MX Record</option>
          <option value="TXT">Create TXT Record</option>
        </select>
      </div>

      <div class="mb-3">
        <label for="domain">
          Domain:
          <InfoIcon>Enter the domain name you want to manage.</InfoIcon>
        </label>
        <input class="form-control" type="text" id="domain" v-model="domain" required />
      </div>

      <div class="mb-3" v-if="selectedAction === 'Claim Domain'">
        <label for="context">
          Context:
          <InfoIcon>Provide a context for the domain claim, such as the project or campaign name.</InfoIcon>
        </label>
        <input class="form-control" placeholder="Context such as 2023 Fall" type="text" id="context" v-model="formInput" required/>
      </div>

      <div class="mb-3" v-if="selectedAction === 'A'">
        <label for="values">
          Enter one or more IPv4 Addresses (one per line):
          <InfoIcon>Provide the IPv4 addresses for the A record. Each address should be on a new line.</InfoIcon>
        </label>
        <textarea id="values" class="form-control mb-3" :placeholder="'1.1.1.1\n2.2.2.2'" v-model="formInput" required></textarea>
        <label for="ttl">
          TTL (seconds):
          <InfoIcon>Time to Live (TTL) in seconds. It defines the duration for which the record is cached.</InfoIcon>
        </label>
        <input class="form-control" placeholder="3600" type="number" v-model="ttl" required/>
      </div>

      <div class="mb-3" v-if="selectedAction === 'AAAA'">
        <label for="values">
          Enter one or more IPv6 Addresses (one per line):
          <InfoIcon>Provide the IPv6 addresses for the AAAA record. Each address should be on a new line.</InfoIcon>
        </label>
        <textarea id="values" class="form-control mb-3" :placeholder="'1111::1111\n2222::2222'" v-model="formInput" required></textarea>
        <label for="ttl">
          TTL (seconds):
          <InfoIcon>Time to Live (TTL) in seconds. It defines the duration for which the record is cached.</InfoIcon>
        </label>
        <input class="form-control" placeholder="3600" type="number" v-model="ttl" required/>
      </div>

      <div class="mb-3" v-if="selectedAction === 'CNAME'">
        <label for="cname">
          Target:
          <InfoIcon>Specify the target domain for the CNAME record.</InfoIcon>
        </label>
        <input class="form-control mb-3"  type="text" id="cname" placeholder="example.com" v-model="formInput" required/>
        <label for="ttl">
          TTL (seconds):
          <InfoIcon>Time to Live (TTL) in seconds. It defines the duration for which the record is cached.</InfoIcon>
        </label>
        <input class="form-control" placeholder="3600" type="number" v-model="ttl" required/>
      </div>

      <div class="mb-3" v-if="selectedAction === 'MX'">
        <label for="values">
          Exchange server, preference (one per line):
          <InfoIcon>Provide the mail exchange servers and their preference values, one per line.</InfoIcon>
        </label>
        <textarea id="values" class="form-control mb-3" :placeholder="'mailinator.com, 1\nmail.exchange.com, 3'" v-model="formInput" required></textarea>
        <label for="ttl">
          TTL (seconds):
          <InfoIcon>Time to Live (TTL) in seconds. It defines the duration for which the record is cached.</InfoIcon>
        </label>
        <input class="form-control" placeholder="3600" type="number" v-model="ttl" required/>
      </div>

      <div class="mb-3" v-if="selectedAction === 'TXT'">
        <label for="values">
          Values (one per line):
          <InfoIcon>Provide the TXT record values, each on a new line.</InfoIcon>
        </label>
        <textarea id="values" class="form-control mb-3" :placeholder="'A TXT Record\nAnother value'" v-model="formInput" required></textarea>
        <label for="ttl">
          TTL (seconds):
          <InfoIcon>Time to Live (TTL) in seconds. It defines the duration for which the record is cached.</InfoIcon>
        </label>
        <input class="form-control" placeholder="3600" type="number" v-model="ttl" required/>
      </div>

      <button :disabled="loading" class="form-control btn btn-success" type="submit">
        <div v-if="loading">
          <span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span>
          <span class="sr-only"> Loading...</span>
        </div>
        <span v-else>Submit</span>
      </button>
    </form>
  </div>
</template>

<script>
import axios from "axios";
import InfoIcon from '@/components/InfoIcon.vue';

export default {
  data() {
    return {
      selectedAction: "Claim Domain",
      domain: "",
      loading: false,
      formInput: "",
      ttl: 3600,
    };
  },
  components: {
    InfoIcon
  },
  methods: {
    resetForm() {
      this.domain = "";
      this.formInput = "";
      this.ttl = 3600;
    },
    async submitForm() {
      try {
        // Process the form submission
        let response;
        let values = [];
        this.loading = true;

        switch (this.selectedAction) {
          case "Claim Domain":
            response = await axios.post(
              `${process.env.VUE_APP_API_ORIGIN}/api/zone/${this.domain}`,
              { "context": this.formInput },
              {
                headers: {
                  "Content-Type": "application/json",
                  "Accept": "application/json, */*;q=0.1",
                },
              }
            );
            break;

          case "A":
            values = this.formInput.split("\n");
            values = values.map((value) => value.trim());
            response = await axios.post(
              `${process.env.VUE_APP_API_ORIGIN}/api/zone/${this.domain}/A`,
              { 
                "values": values,
                "ttl": this.ttl
              },
              {
                headers: {
                  "Content-Type": "application/json",
                  "Accept": "application/json, */*;q=0.1",
                },
              }
            );
            break;

          case "AAAA":
            values = this.formInput.split("\n");
            values = values.map((value) => value.trim());
            response = await axios.post(
              `${process.env.VUE_APP_API_ORIGIN}/api/zone/${this.domain}/AAAA`,
              { 
                "values": values,
                "ttl": this.ttl
              },
              {
                headers: {
                  "Content-Type": "application/json",
                  "Accept": "application/json, */*;q=0.1",
                },
              }
            );
            break;

          case "CNAME":
            response = await axios.post(
              `${process.env.VUE_APP_API_ORIGIN}/api/zone/${this.domain}/CNAME`,
              { 
                "cname": this.formInput,
                "ttl": this.ttl
              },
              {
                headers: {
                  "Content-Type": "application/json",
                  "Accept": "application/json, */*;q=0.1",
                },
              }
            );
            break;

          case "MX":
            values = this.formInput.split("\n");
            values = values.map((value) => value.trim().split(","));
            values = values.map(([exchange, preference]) => ({
              preference: preference.trim(),
              exchange: exchange.trim(),
            }));
            response = await axios.post(
              `${process.env.VUE_APP_API_ORIGIN}/api/zone/${this.domain}/MX`,
              { 
                "values": values,
                "ttl": this.ttl
              },
              {
                headers: {
                  "Content-Type": "application/json",
                  "Accept": "application/json, */*;q=0.1",
                },
              }
            );
            break;

          case "TXT":
            values = this.formInput.split("\n");
            response = await axios.post(
              `${process.env.VUE_APP_API_ORIGIN}/api/zone/${this.domain}/TXT`,
              { 
                "values": values,
                "ttl": this.ttl
              },
              {
                headers: {
                  "Content-Type": "application/json",
                  "Accept": "application/json, */*;q=0.1",
                },
              }
            );
            break;
        }

        this.loading = false;
        this.resetForm();
        if (response.status === 201) {
          this.$emit("getZone");
          // Make a success alert
          this.$emit("showSuccess", this.selectedAction)
        }
      } catch (error) {
        this.loading = false;
        // Make an error alert
        this.$emit("showError", this.selectedAction, error)
      }
    },
  },
};
</script>

<style>
form {
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  background-color: #f5f5f5;
}

form button {
  margin-top: 10px;
  width: 100%;
}

textarea {
  height: 150px;
}
</style>
