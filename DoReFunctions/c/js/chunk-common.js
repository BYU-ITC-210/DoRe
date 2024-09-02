"use strict";(self["webpackChunkdns_frontend"]=self["webpackChunkdns_frontend"]||[]).push([[64],{6128:function(e,t,a){var o=a(9242),n=a(3396);function l(e,t){const a=(0,n.up)("router-view");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n.Wm)(a)])}var r=a(89);const s={},d=(0,r.Z)(s,[["render",l]]);var i=d,c=a(2483);function u(e,t,a,o,l,r){const s=(0,n.up)("DNSRecords");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n.Wm)(s)])}var m=a(7139);const p=e=>((0,n.dD)("data-v-e20cc0e6"),e=e(),(0,n.Cn)(),e),f={key:0,id:"overlay"},b=p((()=>(0,n._)("div",{id:"overlay-content"},[(0,n._)("div",{class:"spinner-border",role:"status"},[(0,n._)("span",{class:"sr-only"})]),(0,n._)("div",null,"Loading...")],-1))),h=[b],v={id:"alerts",class:"fixed-top"},y={key:0,id:"negative-alert-popup",class:"alert alert-danger",role:"alert"},w={key:1,id:"positive-alert-popup",class:"alert alert-success",role:"alert"},k={key:2},D=p((()=>(0,n._)("h1",{class:"mt-5"},"DNS Records",-1))),_={class:"container"};function T(e,t,a,o,l,r){const s=(0,n.up)("Action"),d=(0,n.up)("EditRecord"),i=(0,n.up)("Modal"),c=(0,n.up)("Domains"),u=(0,n.up)("ARecords"),p=(0,n.up)("AAAARecords"),b=(0,n.up)("CNAMERecords"),T=(0,n.up)("MXRecords"),g=(0,n.up)("TXTRecords");return(0,n.wg)(),(0,n.iD)("div",null,[e.zoneLoading?((0,n.wg)(),(0,n.iD)("div",f,h)):(0,n.kq)("",!0),(0,n._)("div",v,[e.negativeAlert?((0,n.wg)(),(0,n.iD)("div",y,(0,m.zw)(e.alertText),1)):(0,n.kq)("",!0),e.positiveAlert?((0,n.wg)(),(0,n.iD)("div",w,(0,m.zw)(e.alertText),1)):(0,n.kq)("",!0)]),(0,n.Wm)(i,{showModal:e.showModal,loading:e.loading,hideFooter:e.addingRecord||e.editingRecord,onCloseModal:e.handleCloseModal},{title:(0,n.w5)((()=>[(0,n.Uk)((0,m.zw)(e.modalTitle),1)])),default:(0,n.w5)((()=>[e.addingRecord?((0,n.wg)(),(0,n.j4)(s,{key:0,onGetZone:e.getZone,onShowSuccess:e.showSuccess,onShowError:e.showError},null,8,["onGetZone","onShowSuccess","onShowError"])):e.editingRecord?((0,n.wg)(),(0,n.j4)(d,{key:1,onGetZone:e.getZone,onShowSuccess:e.showSuccess,onShowError:e.showError,record:e.editRecord,type:e.editType},null,8,["onGetZone","onShowSuccess","onShowError","record","type"])):((0,n.wg)(),(0,n.iD)("p",k,(0,m.zw)(e.deleteMessage),1))])),_:1},8,["showModal","loading","hideFooter","onCloseModal"]),D,(0,n._)("div",_,[(0,n._)("button",{class:"btn btn-success mb-3",onClick:t[0]||(t[0]=(...t)=>e.addRecord&&e.addRecord(...t))},"Add Record"),(0,n.Wm)(c,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.domains,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,n.Wm)(u,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.aRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,n.Wm)(p,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.aaaaRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,n.Wm)(b,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.cnameRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,n.Wm)(T,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.mxRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,n.Wm)(g,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.txtRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"])])])}var g=a(4870);const A=["aria-labelledby","aria-hidden"],I={class:"modal-dialog modal-dialog-centered",role:"document"},C={class:"modal-content"},R={class:"modal-header"},E=["id"],U=["disabled"],M={class:"modal-body text-dark"},q={key:0,class:"modal-footer text-dark"},x=["disabled"],Z={key:0},$=(0,n._)("span",{class:"spinner-border spinner-border-sm",role:"status","aria-hidden":"true"},null,-1),S=(0,n._)("span",{class:"sr-only"}," Loading...",-1),L=[$,S],W={key:1},N=["disabled"];function X(e,t,a,o,l,r){return(0,n.wg)(),(0,n.j4)(n.lR,{to:"body"},[(0,n._)("div",{ref:"modal",class:(0,m.C_)(["modal fade",{show:o.active,"d-block":o.active}]),tabindex:"-1",role:"dialog","aria-labelledby":`modal-${o.id}`,"aria-hidden":o.active},[(0,n._)("div",I,[(0,n._)("div",C,[(0,n._)("div",R,[(0,n._)("h5",{class:"modal-title text-dark",id:`modal-${o.id}`},[(0,n.WI)(e.$slots,"title")],8,E),(0,n._)("button",{disabled:a.loading,type:"button",class:"btn-close","data-dismiss":"modal","aria-label":"Close",onClick:t[0]||(t[0]=t=>e.$emit("closeModal",!1))},null,8,U)]),(0,n._)("div",M,[(0,n.WI)(e.$slots,"default")]),a.hideFooter?(0,n.kq)("",!0):((0,n.wg)(),(0,n.iD)("div",q,[(0,n._)("button",{disabled:a.loading,type:"button",class:"btn btn-danger",onClick:t[1]||(t[1]=t=>e.$emit("closeModal",!0))},[a.loading?((0,n.wg)(),(0,n.iD)("div",Z,L)):((0,n.wg)(),(0,n.iD)("span",W,"Yes"))],8,x),(0,n._)("button",{disabled:a.loading,type:"button",class:"btn btn-secondary",onClick:t[2]||(t[2]=t=>e.$emit("closeModal",!1))},"No",8,N)]))])])],10,A),(0,n._)("div",{onClick:t[3]||(t[3]=t=>e.$emit("closeModal",!1)),class:(0,m.C_)(["fade",{show:o.active,"modal-backdrop show":o.active}])},null,2)])}var j={name:"Modal",emits:["closeModal"],props:{showModal:Boolean,modalId:String,loading:Boolean,hideFooter:{type:Boolean,default:!1}},setup(e){const t=()=>{let e=0;return()=>e++},a=(0,g.iH)(e.showModal);return(0,n.YP)((()=>e.showModal),((t,o)=>{if(t!==o){a.value=e.showModal;const t=document.querySelector("body");e.showModal?t.classList.add("modal-open"):t.classList.remove("modal-open")}}),{immediate:!0,deep:!0}),{active:a,id:t}}};const V=(0,r.Z)(j,[["render",X]]);var z=V;const H={class:"heading-container"},F={for:"Domains"};function P(e,t,a,o,l,r){const s=(0,n.up)("InfoIcon"),d=(0,n.up)("TableComponent");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n._)("div",H,[(0,n._)("h2",null,[(0,n.Uk)("Domains "),(0,n._)("label",F,[(0,n.Wm)(s,null,{default:(0,n.w5)((()=>[(0,n.Uk)("This is the domain name record. For example, https://learningsuite.byu.edu, learningsuite is the domain name. ")])),_:1})])])]),(0,n.Wm)(d,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}const B={class:"container"},Y={class:"table-container my-2"},G={id:"tableComponent",class:"table b-table table-bordered table-striped"},O=["onClick","title","aria-sort"],K={key:0},Q={key:1},J={key:0},ee=["onClick"],te=["onClick"],ae={key:1},oe=["colspan"];function ne(e,t,a,o,l,r){return(0,n.wg)(),(0,n.iD)("div",B,[(0,n._)("div",Y,[(0,n._)("table",G,[(0,n._)("thead",null,[(0,n._)("tr",null,[((0,n.wg)(!0),(0,n.iD)(n.HY,null,(0,n.Ko)(e.fields,(t=>((0,n.wg)(),(0,n.iD)("th",{key:t.key,onClick:a=>t.sortable?e.sortTable(t.key):"",title:t.sortable?`Sort by ${t.label}`:t.label,"aria-sort":t.sortable?e.getAriaSort(t.key):""},(0,m.zw)(t.label),9,O)))),128)),e.tableData.length?((0,n.wg)(),(0,n.iD)("th",K,"Edit")):(0,n.kq)("",!0),e.tableData.length?((0,n.wg)(),(0,n.iD)("th",Q,"Delete")):(0,n.kq)("",!0)])]),e.tableData.length>0?((0,n.wg)(),(0,n.iD)("tbody",J,[((0,n.wg)(!0),(0,n.iD)(n.HY,null,(0,n.Ko)(e.tableData,(t=>((0,n.wg)(),(0,n.iD)("tr",{key:t},[((0,n.wg)(!0),(0,n.iD)(n.HY,null,(0,n.Ko)(e.fields,(e=>((0,n.wg)(),(0,n.iD)("td",{key:e.key},(0,m.zw)(e.formatter?e.formatter(t[e.key]):t[e.key]),1)))),128)),(0,n._)("td",null,[(0,n._)("button",{class:"btn btn-secondary",onClick:a=>e.$emit("edit",t)}," Edit ",8,ee)]),(0,n._)("td",null,[(0,n._)("button",{class:"btn btn-danger",onClick:a=>e.$emit("delete",t)}," Delete ",8,te)])])))),128))])):((0,n.wg)(),(0,n.iD)("tbody",ae,[(0,n._)("tr",null,[(0,n._)("td",{colspan:e.fields.length,class:"text-center"}," No results found ",8,oe)])]))])])])}var le=(0,n.aZ)({name:"TableComponent",props:{tableData:{type:Array,required:!0},fields:{type:Array,required:!0},sortBy:{type:String,default:""}},setup(e){const t=(0,g.iH)(""),a=(0,g.iH)(!0);(0,n.YP)((()=>e.tableData),(()=>{if(t.value){let o=t.value;a.value?e.tableData.sort(((e,t)=>e[o]>t[o]?1:-1)):e.tableData.sort(((e,t)=>e[o]<t[o]?1:-1))}else if(e.sortBy&&e.fields.find((t=>t.key===e.sortBy)))o(e.sortBy);else{let t=e.fields.find((e=>e.sortable));t&&o(t.key)}}));const o=o=>{t.value===o?(e.tableData.reverse(),a.value=!a.value):(e.tableData.sort(((e,t)=>e[o]>t[o]?1:-1)),t.value=o,a.value=!0)},l=e=>{const o=a.value?"ascending":"descending";return t.value===e?o:"none"},r=e=>{this.$emit("delete",e)};return{sortTable:o,getAriaSort:l,deleteItem:r}}});const re=(0,r.Z)(le,[["render",ne],["__scopeId","data-v-133eb1e4"]]);var se=re;const de={class:"info-icon",ref:"infoIcon"},ie={key:0,class:"info-content",ref:"infoContent"};function ce(e,t,a,o,l,r){const s=(0,n.Q2)("closable");return(0,n.wg)(),(0,n.iD)("div",de,[(0,n._)("span",{onClick:t[0]||(t[0]=(...t)=>e.toggleInfo&&e.toggleInfo(...t)),class:"icon"},"❔"),e.showInfo?(0,n.wy)(((0,n.wg)(),(0,n.iD)("div",ie,[(0,n.WI)(e.$slots,"default",{},void 0,!0)])),[[s,{handler:"onClose",exclude:["infoIcon","infoContent"]}]]):(0,n.kq)("",!0)],512)}const ue={mounted(e,t){const a=a=>{const{handler:o,exclude:n}=t.value;let l=!1;n.forEach((e=>{const o=t.instance.$refs[e];o&&o.contains(a.target)&&(l=!0)})),e.contains(a.target)||l||t.instance[o]()};setTimeout((()=>{document.addEventListener("click",a),document.addEventListener("touchstart",a)}),100),e._handleOutsideClick=a},unmounted(e){document.removeEventListener("click",e._handleOutsideClick),document.removeEventListener("touchstart",e._handleOutsideClick)}};var me=(0,n.aZ)({name:"InfoIcon",data(){return{showInfo:!1}},methods:{toggleInfo(){this.showInfo=!this.showInfo},onClose(){this.showInfo=!1}},directives:{closable:ue},mounted(){this.$nextTick((()=>{this.showInfo&&this.$refs.infoContent.focus()}))}});const pe=(0,r.Z)(me,[["render",ce],["__scopeId","data-v-09576fae"]]);var fe=pe,be=(0,n.aZ)({name:"Domains",components:{TableComponent:se,InfoIcon:fe},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"context",sortable:!0,label:"Context"},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime}],o=async e=>{t("delete",e.name,"domain")},n=async e=>{t("edit",e,"domain")};return{fields:a,deleteRecord:o,editRecord:n}}});const he=(0,r.Z)(be,[["render",P]]);var ve=he;const ye={class:"heading-container"},we={for:"A Records"};function ke(e,t,a,o,l,r){const s=(0,n.up)("InfoIcon"),d=(0,n.up)("TableComponent");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n._)("div",ye,[(0,n._)("h2",null,[(0,n.Uk)("A Records "),(0,n._)("label",we,[(0,n.Wm)(s,null,{default:(0,n.w5)((()=>[(0,n.Uk)("An A record indicates the IP address of the domain. A records hold IPv4 addresses. A stands for address in this case.")])),_:1})])])]),(0,n.Wm)(d,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var De=(0,n.aZ)({name:"ARecords",components:{TableComponent:se,InfoIcon:fe},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"IPv4 Address(s)",formatter:e=>e.join(" | ")},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"A")},n=async e=>{t("edit",e,"A")};return{fields:a,deleteRecord:o,editRecord:n}}});const _e=(0,r.Z)(De,[["render",ke]]);var Te=_e;const ge={class:"heading-container"},Ae={for:"AAAA Records"};function Ie(e,t,a,o,l,r){const s=(0,n.up)("InfoIcon"),d=(0,n.up)("TableComponent");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n._)("div",ge,[(0,n._)("h2",null,[(0,n.Uk)("AAAA Records "),(0,n._)("label",Ae,[(0,n.Wm)(s,null,{default:(0,n.w5)((()=>[(0,n.Uk)("AAAA Records match a domain name to an IPv6 address, like what an A record does for an IPv4 address.")])),_:1})])])]),(0,n.Wm)(d,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var Ce=(0,n.aZ)({name:"AAAARecords",components:{TableComponent:se,InfoIcon:fe},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"IPv6 Address(s)",formatter:e=>e.join(" | ")},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"AAAA")},n=async e=>{t("edit",e,"AAAA")};return{fields:a,deleteRecord:o,editRecord:n}}});const Re=(0,r.Z)(Ce,[["render",Ie]]);var Ee=Re;const Ue={class:"heading-container"},Me={for:"CNAME Records"};function qe(e,t,a,o,l,r){const s=(0,n.up)("InfoIcon"),d=(0,n.up)("TableComponent");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n._)("div",Ue,[(0,n._)("h2",null,[(0,n.Uk)("CNAME Records "),(0,n._)("label",Me,[(0,n.Wm)(s,null,{default:(0,n.w5)((()=>[(0,n.Uk)("A CNAME record points to an alias for a different domain. For example, you could have a CNAME record for learningsuite.com that points to home.learningsuite.com. ")])),_:1})])])]),(0,n.Wm)(d,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var xe=(0,n.aZ)({name:"CNAMERecords",components:{TableComponent:se,InfoIcon:fe},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"cname",sortable:!1,label:"CNAME"},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"CNAME")},n=async e=>{t("edit",e,"CNAME")};return{fields:a,deleteRecord:o,editRecord:n}}});const Ze=(0,r.Z)(xe,[["render",qe]]);var $e=Ze;const Se={class:"heading-container"},Le={for:"MX Records"};function We(e,t,a,o,l,r){const s=(0,n.up)("InfoIcon"),d=(0,n.up)("TableComponent");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n._)("div",Se,[(0,n._)("h2",null,[(0,n.Uk)("MX Records "),(0,n._)("label",Le,[(0,n.Wm)(s,null,{default:(0,n.w5)((()=>[(0,n.Uk)("An MX records redirects email to a mail server.")])),_:1})])])]),(0,n.Wm)(d,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var Ne=(0,n.aZ)({name:"MXRecords",components:{TableComponent:se,InfoIcon:fe},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"Values",formatter:e=>{let t=e.map((e=>`${e.exchange} (Preference ${e.preference})`));return t.join(" | ")}},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"MX")},n=async e=>{t("edit",e,"MX")};return{fields:a,deleteRecord:o,editRecord:n}}});const Xe=(0,r.Z)(Ne,[["render",We]]);var je=Xe;const Ve={class:"heading-container"},ze={for:"TXT Records"};function He(e,t,a,o,l,r){const s=(0,n.up)("InfoIcon"),d=(0,n.up)("TableComponent");return(0,n.wg)(),(0,n.iD)("div",null,[(0,n._)("div",Ve,[(0,n._)("h2",null,[(0,n.Uk)("TXT Records "),(0,n._)("label",ze,[(0,n.Wm)(s,null,{default:(0,n.w5)((()=>[(0,n.Uk)("A TXT record lets administrators enter text into the DNS system. It can be used to prevent email spam and prove domain ownership. ")])),_:1})])])]),(0,n.Wm)(d,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var Fe=(0,n.aZ)({name:"TXTRecords",components:{TableComponent:se,InfoIcon:fe},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"Values",formatter:e=>e.join(" | ")},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"TXT")},n=async e=>{t("edit",e,"TXT")};return{fields:a,deleteRecord:o,editRecord:n}}});const Pe=(0,r.Z)(Fe,[["render",He]]);var Be=Pe;const Ye={class:"container"},Ge={class:"mb-3"},Oe={for:"action"},Ke=(0,n.uE)('<option value="Claim Domain">Claim Domain</option><option value="A">Create A Record</option><option value="AAAA">Create AAAA Record</option><option value="CNAME">Create CNAME Record</option><option value="MX">Create MX Record</option><option value="TXT">Create TXT Record</option>',6),Qe=[Ke],Je={class:"mb-3"},et={for:"domain"},tt={key:0,class:"mb-3"},at={for:"context"},ot={key:1,class:"mb-3"},nt={for:"values"},lt={for:"ttl"},rt={key:2,class:"mb-3"},st={for:"values"},dt={for:"ttl"},it={key:3,class:"mb-3"},ct={for:"cname"},ut={for:"ttl"},mt={key:4,class:"mb-3"},pt={for:"values"},ft=["placeholder"],bt={for:"ttl"},ht={key:5,class:"mb-3"},vt={for:"values"},yt={for:"ttl"},wt=["disabled"],kt={key:0},Dt=(0,n._)("span",{class:"spinner-border spinner-border-sm",role:"status","aria-hidden":"true"},null,-1),_t=(0,n._)("span",{class:"sr-only"}," Loading...",-1),Tt=[Dt,_t],gt={key:1};function At(e,t,a,l,r,s){const d=(0,n.up)("InfoIcon");return(0,n.wg)(),(0,n.iD)("div",Ye,[(0,n._)("form",{onSubmit:t[14]||(t[14]=(0,o.iM)(((...e)=>s.submitForm&&s.submitForm(...e)),["prevent"]))},[(0,n._)("div",Ge,[(0,n._)("label",Oe,[(0,n.Uk)(" Action: "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Select the type of DNS record action to perform.")])),_:1})]),(0,n.wy)((0,n._)("select",{class:"form-select",id:"action","onUpdate:modelValue":t[0]||(t[0]=e=>r.selectedAction=e),onChange:t[1]||(t[1]=(...e)=>s.resetForm&&s.resetForm(...e))},Qe,544),[[o.bM,r.selectedAction]])]),(0,n._)("div",Je,[(0,n._)("label",et,[(0,n.Uk)(" Domain: "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Enter the domain name you want to manage.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control",type:"text",id:"domain","onUpdate:modelValue":t[2]||(t[2]=e=>r.domain=e),required:""},null,512),[[o.nr,r.domain]])]),"Claim Domain"===r.selectedAction?((0,n.wg)(),(0,n.iD)("div",tt,[(0,n._)("label",at,[(0,n.Uk)(" Context: "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Provide a context for the domain claim, such as the project or campaign name.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"Context such as 2023 Fall",type:"text",id:"context","onUpdate:modelValue":t[3]||(t[3]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]])])):(0,n.kq)("",!0),"A"===r.selectedAction?((0,n.wg)(),(0,n.iD)("div",ot,[(0,n._)("label",nt,[(0,n.Uk)(" Enter one or more IPv4 Addresses (one per line): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Provide the IPv4 addresses for the A record. Each address should be on a new line.")])),_:1})]),(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1.1.1.1\n2.2.2.2","onUpdate:modelValue":t[4]||(t[4]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),(0,n._)("label",lt,[(0,n.Uk)(" TTL (seconds): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Time to Live (TTL) in seconds. It defines the duration for which the record is cached.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[5]||(t[5]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,n.kq)("",!0),"AAAA"===r.selectedAction?((0,n.wg)(),(0,n.iD)("div",rt,[(0,n._)("label",st,[(0,n.Uk)(" Enter one or more IPv6 Addresses (one per line): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Provide the IPv6 addresses for the AAAA record. Each address should be on a new line.")])),_:1})]),(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1111::1111\n2222::2222","onUpdate:modelValue":t[6]||(t[6]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),(0,n._)("label",dt,[(0,n.Uk)(" TTL (seconds): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Time to Live (TTL) in seconds. It defines the duration for which the record is cached.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[7]||(t[7]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,n.kq)("",!0),"CNAME"===r.selectedAction?((0,n.wg)(),(0,n.iD)("div",it,[(0,n._)("label",ct,[(0,n.Uk)(" Target: "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Specify the target domain for the CNAME record.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control mb-3",type:"text",id:"cname",placeholder:"example.com","onUpdate:modelValue":t[8]||(t[8]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),(0,n._)("label",ut,[(0,n.Uk)(" TTL (seconds): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Time to Live (TTL) in seconds. It defines the duration for which the record is cached.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[9]||(t[9]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,n.kq)("",!0),"MX"===r.selectedAction?((0,n.wg)(),(0,n.iD)("div",mt,[(0,n._)("label",pt,[(0,n.Uk)(" Exchange server, preference (one per line): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Provide the mail exchange servers and their preference values, one per line.")])),_:1})]),(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"mailinator.com, 1\nmail.exchange.com, 3","onUpdate:modelValue":t[10]||(t[10]=e=>r.formInput=e),required:""},null,8,ft),[[o.nr,r.formInput]]),(0,n._)("label",bt,[(0,n.Uk)(" TTL (seconds): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Time to Live (TTL) in seconds. It defines the duration for which the record is cached.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[11]||(t[11]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,n.kq)("",!0),"TXT"===r.selectedAction?((0,n.wg)(),(0,n.iD)("div",ht,[(0,n._)("label",vt,[(0,n.Uk)(" Values (one per line): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Provide the TXT record values, each on a new line.")])),_:1})]),(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"A TXT Record\nAnother value","onUpdate:modelValue":t[12]||(t[12]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),(0,n._)("label",yt,[(0,n.Uk)(" TTL (seconds): "),(0,n.Wm)(d,null,{default:(0,n.w5)((()=>[(0,n.Uk)("Time to Live (TTL) in seconds. It defines the duration for which the record is cached.")])),_:1})]),(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[13]||(t[13]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,n.kq)("",!0),(0,n._)("button",{disabled:r.loading,class:"form-control btn btn-success",type:"submit"},[r.loading?((0,n.wg)(),(0,n.iD)("div",kt,Tt)):((0,n.wg)(),(0,n.iD)("span",gt,"Submit"))],8,wt)],32)])}var It=a(4161),Ct={data(){return{selectedAction:"Claim Domain",domain:"",loading:!1,formInput:"",ttl:3600}},components:{InfoIcon:fe},methods:{resetForm(){this.domain="",this.formInput="",this.ttl=3600},async submitForm(){try{let e,t=[];switch(this.loading=!0,this.selectedAction){case"Claim Domain":e=await It.Z.post(`/api/zone/${this.domain}`,{context:this.formInput},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"A":t=this.formInput.split("\n"),t=t.map((e=>e.trim())),e=await It.Z.post(`/api/zone/${this.domain}/A`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"AAAA":t=this.formInput.split("\n"),t=t.map((e=>e.trim())),e=await It.Z.post(`/api/zone/${this.domain}/AAAA`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"CNAME":e=await It.Z.post(`/api/zone/${this.domain}/CNAME`,{cname:this.formInput,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"MX":t=this.formInput.split("\n"),t=t.map((e=>e.trim().split(","))),t=t.map((([e,t])=>({preference:t.trim(),exchange:e.trim()}))),e=await It.Z.post(`/api/zone/${this.domain}/MX`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"TXT":t=this.formInput.split("\n"),e=await It.Z.post(`/api/zone/${this.domain}/TXT`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break}this.loading=!1,this.resetForm(),201===e.status&&(this.$emit("getZone"),this.$emit("showSuccess",this.selectedAction))}catch(e){this.loading=!1,this.$emit("showError",this.selectedAction,e)}}}};const Rt=(0,r.Z)(Ct,[["render",At]]);var Et=Rt;const Ut={class:"container"},Mt={class:"mb-3"},qt=(0,n._)("label",{for:"domain"},"Domain:",-1),xt={key:0,class:"mb-3"},Zt=(0,n._)("label",{for:"context"},"Context:",-1),$t={key:1,class:"mb-3"},St=(0,n._)("label",{for:"values"},"Enter one or more IPv4 Addresses (one per line):",-1),Lt=(0,n._)("label",{for:"ttl"},"TTL (seconds):",-1),Wt={key:2,class:"mb-3"},Nt=(0,n._)("label",{for:"values"},"Enter one or more IPv6 Addresses (one per line):",-1),Xt=(0,n._)("label",{for:"ttl"},"TTL (seconds):",-1),jt={key:3,class:"mb-3"},Vt=(0,n._)("label",{for:"cname"},"Target:",-1),zt=(0,n._)("label",{for:"ttl"},"TTL (seconds):",-1),Ht={key:4,class:"mb-3"},Ft=(0,n._)("label",{for:"values"},"Exchange server, preference (one per line):",-1),Pt=["placeholder"],Bt=(0,n._)("label",{for:"ttl"},"TTL (seconds):",-1),Yt={key:5,class:"mb-3"},Gt=(0,n._)("label",{for:"values"},"Values (one per line):",-1),Ot=(0,n._)("label",{for:"ttl"},"TTL (seconds):",-1),Kt=["disabled"],Qt={key:0},Jt=(0,n._)("span",{class:"spinner-border spinner-border-sm",role:"status","aria-hidden":"true"},null,-1),ea=(0,n._)("span",{class:"sr-only"}," Loading...",-1),ta=[Jt,ea],aa={key:1};function oa(e,t,a,l,r,s){return(0,n.wg)(),(0,n.iD)("div",Ut,[(0,n._)("form",{onSubmit:t[12]||(t[12]=(0,o.iM)(((...e)=>l.submitForm&&l.submitForm(...e)),["prevent"]))},[(0,n._)("div",Mt,[qt,(0,n.wy)((0,n._)("input",{disabled:"",class:"form-control",type:"text",id:"domain","onUpdate:modelValue":t[0]||(t[0]=e=>l.domain=e),required:""},null,512),[[o.nr,l.domain]])]),"domain"===a.type?((0,n.wg)(),(0,n.iD)("div",xt,[Zt,(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"Context such as 2023 Fall",type:"text",id:"context","onUpdate:modelValue":t[1]||(t[1]=e=>l.formInput=e),required:""},null,512),[[o.nr,l.formInput]])])):(0,n.kq)("",!0),"A"===a.type?((0,n.wg)(),(0,n.iD)("div",$t,[St,(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1.1.1.1\n2.2.2.2","onUpdate:modelValue":t[2]||(t[2]=e=>l.formInput=e),required:""},null,512),[[o.nr,l.formInput]]),Lt,(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[3]||(t[3]=e=>l.ttl=e),required:""},null,512),[[o.nr,l.ttl]])])):(0,n.kq)("",!0),"AAAA"===a.type?((0,n.wg)(),(0,n.iD)("div",Wt,[Nt,(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1111::1111\n2222::2222","onUpdate:modelValue":t[4]||(t[4]=e=>l.formInput=e),required:""},null,512),[[o.nr,l.formInput]]),Xt,(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[5]||(t[5]=e=>l.ttl=e),required:""},null,512),[[o.nr,l.ttl]])])):(0,n.kq)("",!0),"CNAME"===a.type?((0,n.wg)(),(0,n.iD)("div",jt,[Vt,(0,n.wy)((0,n._)("input",{class:"form-control mb-3",type:"text",id:"cname",placeholder:"example.com","onUpdate:modelValue":t[6]||(t[6]=e=>l.formInput=e),required:""},null,512),[[o.nr,l.formInput]]),zt,(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[7]||(t[7]=e=>l.ttl=e),required:""},null,512),[[o.nr,l.ttl]])])):(0,n.kq)("",!0),"MX"===a.type?((0,n.wg)(),(0,n.iD)("div",Ht,[Ft,(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"mailinator.com, 1\nmail.exchange.com, 3","onUpdate:modelValue":t[8]||(t[8]=e=>l.formInput=e),required:""},null,8,Pt),[[o.nr,l.formInput]]),Bt,(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[9]||(t[9]=e=>l.ttl=e),required:""},null,512),[[o.nr,l.ttl]])])):(0,n.kq)("",!0),"TXT"===a.type?((0,n.wg)(),(0,n.iD)("div",Yt,[Gt,(0,n.wy)((0,n._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"A TXT Record\nAnother value","onUpdate:modelValue":t[10]||(t[10]=e=>l.formInput=e),required:""},null,512),[[o.nr,l.formInput]]),Ot,(0,n.wy)((0,n._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[11]||(t[11]=e=>l.ttl=e),required:""},null,512),[[o.nr,l.ttl]])])):(0,n.kq)("",!0),(0,n._)("button",{disabled:l.loading,class:"form-control btn btn-success",type:"submit"},[l.loading?((0,n.wg)(),(0,n.iD)("div",Qt,ta)):((0,n.wg)(),(0,n.iD)("span",aa,"Submit"))],8,Kt)],32)])}var na={props:{record:{},type:""},emits:["getZone","showSuccess","showError"],setup(e,{emit:t}){const a=(0,g.iH)(""),o=(0,g.iH)(!1),l=(0,g.iH)(""),r=(0,g.iH)(3600);(0,n.m0)((()=>{"domain"===e.type?l.value=e.record.context:"CNAME"===e.type?l.value=e.record.cname:"MX"===e.type?l.value=e.record.values.map((e=>`${e.exchange}, ${e.preference}`)).join("\n"):l.value=e.record.values.join("\n"),a.value=e.record.name,r.value=e.record.ttl}));const s=()=>{a.value="",l.value="",r.value=3600},d=async()=>{try{let n,d=[];switch(o.value=!0,e.type){case"domain":n=await It.Z.put(`/api/zone/${a.value}`,{context:l.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"A":case"AAAA":case"TXT":d=l.value.split("\n"),n=await It.Z.put(`/api/zone/${a.value}/${e.type}`,{values:d,ttl:r.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"CNAME":n=await It.Z.put(`/api/zone/${a.value}/${e.type}`,{cname:l.value,ttl:r.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"MX":d=l.value.split("\n").map((e=>{const[t,a]=e.split(",");return{exchange:t,preference:parseInt(a)}})),n=await It.Z.put(`/api/zone/${a.value}/${e.type}`,{values:d,ttl:r.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break}o.value=!1,s(),console.log(n.status),200===n.status&&(t("getZone"),t("showSuccess",e.type,!0))}catch(n){o.value=!1,t("showError",e.type,n,!0)}};return{domain:a,loading:o,formInput:l,ttl:r,resetForm:s,submitForm:d}}};const la=(0,r.Z)(na,[["render",oa]]);var ra=la,sa=(0,n.aZ)({components:{Modal:z,Domains:ve,ARecords:Te,AAAARecords:Ee,CNAMERecords:$e,MXRecords:je,TXTRecords:Be,Action:Et,EditRecord:ra},name:"DNSRecords",setup(){const e=(0,g.iH)({domains:[],aRecords:[],aaaaRecords:[],cnameRecords:[],mxRecords:[],txtRecords:[]}),t=((0,g.iH)(""),(0,g.iH)(!1)),a=(0,g.iH)(""),o=(0,g.iH)(""),l=(0,g.iH)({}),r=(0,g.iH)(""),s=(0,g.iH)(""),d=(0,g.iH)(!1),i=(0,g.iH)(!1),c=(0,g.iH)(!1),u=(0,g.iH)(!1),m=(0,g.iH)(""),p=(0,g.iH)(""),f=(0,g.iH)(!1),b=(0,g.iH)(!1),h=async()=>{i.value=!0;try{const t="/api/zone",a={"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"},o=await It.Z.get(t,{headers:a});200===o.status&&(e.value=o.data)}catch(t){_("fetch",t),console.error("Error fetching data:",t)}i.value=!1},v=(e,n)=>{f.value=!1,b.value=!1,p.value="Delete Record",t.value=!0,a.value=e,o.value=n,s.value="domain"===n?"Deleting a domain will delete all records associated with it. Are you sure you want to continue?":`Are you sure you want to delete this ${n} record?`},y=(e,a)=>{f.value=!1,b.value=!0,p.value="Edit Record",t.value=!0,l.value=e,r.value=a},w=async e=>{if(e&&a.value&&o.value)try{let e="";e="domain"===o.value?`/api/zone/${a.value}`:`/api/zone/${a.value}/${o.value}`,d.value=!0;const t=await It.Z.delete(e);200===t.status&&(D("delete"),await h())}catch(n){d.value=!1,_(`delete ${o.value}`,n)}d.value=!1,t.value=!1,a.value="",o.value="",s.value=""},k=e=>{const t={year:"numeric",month:"short",day:"numeric",hour:"numeric",minute:"numeric"},a=new Date(e);return a.toLocaleString(void 0,t)},D=(e,a=!1)=>{t.value=!1,u.value=!0,m.value="delete"===e?"Record deleted successfully.":`${e} record ${a?"updated":"created"} successfully.`,setTimeout((()=>{m.value="",u.value=!1}),3e3)},_=(e,a,o=!1)=>{t.value=!1,c.value=!0,a.response?.data?.message?m.value="Error: "+a.response.data.message:m.value=`There was an error ${o?"updating":"creating"} the ${e} record.`,setTimeout((()=>{m.value="",c.value=!1}),1e4)},T=()=>{p.value="Add Record",t.value=!0,b.value=!1,f.value=!0};return(0,n.bv)(h),{zoneData:e,showModal:t,deleteName:a,deleteType:o,deleteMessage:s,editRecord:l,editType:r,loading:d,zoneLoading:i,negativeAlert:c,positiveAlert:u,alertText:m,modalTitle:p,addingRecord:f,editingRecord:b,handleDelete:v,handleEdit:y,handleCloseModal:w,formatDateTime:k,showSuccess:D,showError:_,getZone:h,addRecord:T}}});const da=(0,r.Z)(sa,[["render",T],["__scopeId","data-v-e20cc0e6"]]);var ia=da,ca={name:"ZoneView",components:{DNSRecords:ia}};const ua=(0,r.Z)(ca,[["render",u]]);var ma=ua;const pa=[{path:"/",name:"home",component:ma},{path:"/login",name:"login",component:()=>a.e(443).then(a.bind(a,6678))}],fa=(0,c.p7)({history:(0,c.PO)("/c/"),routes:pa});var ba=fa;a(5654);const ha=(0,o.ri)(i);ha.use(ba),It.Z.interceptors.request.use((e=>(e.headers["Authorization"]=`Bearer ${sessionStorage.getItem("token")}`,e)),(e=>Promise.reject(e))),It.Z.interceptors.response.use((e=>{const t=e.headers.get("Authentication-Info");if(t){let e="";for(let a of t.split(",")){a=a.trim();let t=a.search("=");if(t<0)continue;let o=t;while(o>0&&(" "==a[o-1]||"\t"==a[o-1]))--o;if(13==o&&"bearer-update"==a.substring(0,13).toLowerCase()){a.substring(t+1).trim(),e=a.substring(t+1).trim(),sessionStorage.setItem("token",e);break}}}return e})),ha.mount("#app")}}]);
//# sourceMappingURL=chunk-common.js.map