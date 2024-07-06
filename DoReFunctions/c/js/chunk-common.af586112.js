"use strict";(self["webpackChunkdns_frontend"]=self["webpackChunkdns_frontend"]||[]).push([[64],{482:function(e,t,a){var o=a(9242),l=a(3396);function n(e,t){const a=(0,l.up)("router-view");return(0,l.wg)(),(0,l.iD)("div",null,[(0,l.Wm)(a)])}var r=a(89);const s={},d=(0,r.Z)(s,[["render",n]]);var i=d,c=a(2483);function u(e,t,a,o,n,r){const s=(0,l.up)("DNSRecords");return(0,l.wg)(),(0,l.iD)("div",null,[(0,l.Wm)(s)])}var m=a(7139);const p=e=>((0,l.dD)("data-v-e20cc0e6"),e=e(),(0,l.Cn)(),e),b={key:0,id:"overlay"},f=p((()=>(0,l._)("div",{id:"overlay-content"},[(0,l._)("div",{class:"spinner-border",role:"status"},[(0,l._)("span",{class:"sr-only"})]),(0,l._)("div",null,"Loading...")],-1))),v=[f],y={id:"alerts",class:"fixed-top"},h={key:0,id:"negative-alert-popup",class:"alert alert-danger",role:"alert"},D={key:1,id:"positive-alert-popup",class:"alert alert-success",role:"alert"},w={key:2},T=p((()=>(0,l._)("h1",{class:"mt-5"},"DNS Records",-1))),g={class:"container"};function A(e,t,a,o,n,r){const s=(0,l.up)("Action"),d=(0,l.up)("EditRecord"),i=(0,l.up)("Modal"),c=(0,l.up)("Domains"),u=(0,l.up)("ARecords"),p=(0,l.up)("AAAARecords"),f=(0,l.up)("CNAMERecords"),A=(0,l.up)("MXRecords"),_=(0,l.up)("TXTRecords");return(0,l.wg)(),(0,l.iD)("div",null,[e.zoneLoading?((0,l.wg)(),(0,l.iD)("div",b,v)):(0,l.kq)("",!0),(0,l._)("div",y,[e.negativeAlert?((0,l.wg)(),(0,l.iD)("div",h,(0,m.zw)(e.alertText),1)):(0,l.kq)("",!0),e.positiveAlert?((0,l.wg)(),(0,l.iD)("div",D,(0,m.zw)(e.alertText),1)):(0,l.kq)("",!0)]),(0,l.Wm)(i,{showModal:e.showModal,loading:e.loading,hideFooter:e.addingRecord||e.editingRecord,onCloseModal:e.handleCloseModal},{title:(0,l.w5)((()=>[(0,l.Uk)((0,m.zw)(e.modalTitle),1)])),default:(0,l.w5)((()=>[e.addingRecord?((0,l.wg)(),(0,l.j4)(s,{key:0,onGetZone:e.getZone,onShowSuccess:e.showSuccess,onShowError:e.showError},null,8,["onGetZone","onShowSuccess","onShowError"])):e.editingRecord?((0,l.wg)(),(0,l.j4)(d,{key:1,onGetZone:e.getZone,onShowSuccess:e.showSuccess,onShowError:e.showError,record:e.editRecord,type:e.editType},null,8,["onGetZone","onShowSuccess","onShowError","record","type"])):((0,l.wg)(),(0,l.iD)("p",w,(0,m.zw)(e.deleteMessage),1))])),_:1},8,["showModal","loading","hideFooter","onCloseModal"]),T,(0,l._)("div",g,[(0,l._)("button",{class:"btn btn-success mb-3",onClick:t[0]||(t[0]=(...t)=>e.addRecord&&e.addRecord(...t))},"Add Record"),(0,l.Wm)(c,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.domains,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,l.Wm)(u,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.aRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,l.Wm)(p,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.aaaaRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,l.Wm)(f,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.cnameRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,l.Wm)(A,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.mxRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"]),(0,l.Wm)(_,{onDelete:e.handleDelete,onEdit:e.handleEdit,_data:e.zoneData.txtRecords,formatDateTime:e.formatDateTime},null,8,["onDelete","onEdit","_data","formatDateTime"])])])}var _=a(4870);const k=["aria-labelledby","aria-hidden"],C={class:"modal-dialog modal-dialog-centered",role:"document"},R={class:"modal-content"},E={class:"modal-header"},q=["id"],M=["disabled"],x={class:"modal-body text-dark"},I={key:0,class:"modal-footer text-dark"},Z=["disabled"],$={key:0},S=(0,l._)("span",{class:"spinner-border spinner-border-sm",role:"status","aria-hidden":"true"},null,-1),j=(0,l._)("span",{class:"sr-only"}," Loading...",-1),U=[S,j],V={key:1},z=["disabled"];function X(e,t,a,o,n,r){return(0,l.wg)(),(0,l.j4)(l.lR,{to:"body"},[(0,l._)("div",{ref:"modal",class:(0,m.C_)(["modal fade",{show:o.active,"d-block":o.active}]),tabindex:"-1",role:"dialog","aria-labelledby":`modal-${o.id}`,"aria-hidden":o.active},[(0,l._)("div",C,[(0,l._)("div",R,[(0,l._)("div",E,[(0,l._)("h5",{class:"modal-title text-dark",id:`modal-${o.id}`},[(0,l.WI)(e.$slots,"title")],8,q),(0,l._)("button",{disabled:a.loading,type:"button",class:"btn-close","data-dismiss":"modal","aria-label":"Close",onClick:t[0]||(t[0]=t=>e.$emit("closeModal",!1))},null,8,M)]),(0,l._)("div",x,[(0,l.WI)(e.$slots,"default")]),a.hideFooter?(0,l.kq)("",!0):((0,l.wg)(),(0,l.iD)("div",I,[(0,l._)("button",{disabled:a.loading,type:"button",class:"btn btn-danger",onClick:t[1]||(t[1]=t=>e.$emit("closeModal",!0))},[a.loading?((0,l.wg)(),(0,l.iD)("div",$,U)):((0,l.wg)(),(0,l.iD)("span",V,"Yes"))],8,Z),(0,l._)("button",{disabled:a.loading,type:"button",class:"btn btn-secondary",onClick:t[2]||(t[2]=t=>e.$emit("closeModal",!1))},"No",8,z)]))])])],10,k),(0,l._)("div",{onClick:t[3]||(t[3]=t=>e.$emit("closeModal",!1)),class:(0,m.C_)(["fade",{show:o.active,"modal-backdrop show":o.active}])},null,2)])}var N={name:"Modal",emits:["closeModal"],props:{showModal:Boolean,modalId:String,loading:Boolean,hideFooter:{type:Boolean,default:!1}},setup(e){const t=()=>{let e=0;return()=>e++},a=(0,_.iH)(e.showModal);return(0,l.YP)((()=>e.showModal),((t,o)=>{if(t!==o){a.value=e.showModal;const t=document.querySelector("body");e.showModal?t.classList.add("modal-open"):t.classList.remove("modal-open")}}),{immediate:!0,deep:!0}),{active:a,id:t}}};const H=(0,r.Z)(N,[["render",X]]);var L=H;const F=(0,l._)("h2",null,"Domains",-1);function W(e,t,a,o,n,r){const s=(0,l.up)("TableComponent");return(0,l.wg)(),(0,l.iD)("div",null,[F,(0,l.Wm)(s,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}const B={class:"container"},P={class:"table-container my-2"},Y={id:"tableComponent",class:"table b-table table-bordered table-striped"},G=["onClick","title","aria-sort"],K={key:0},O={key:1},J={key:0},Q=["onClick"],ee=["onClick"],te={key:1},ae=["colspan"];function oe(e,t,a,o,n,r){return(0,l.wg)(),(0,l.iD)("div",B,[(0,l._)("div",P,[(0,l._)("table",Y,[(0,l._)("thead",null,[(0,l._)("tr",null,[((0,l.wg)(!0),(0,l.iD)(l.HY,null,(0,l.Ko)(e.fields,(t=>((0,l.wg)(),(0,l.iD)("th",{key:t.key,onClick:a=>t.sortable?e.sortTable(t.key):"",title:t.sortable?`Sort by ${t.label}`:t.label,"aria-sort":t.sortable?e.getAriaSort(t.key):""},(0,m.zw)(t.label),9,G)))),128)),e.tableData.length?((0,l.wg)(),(0,l.iD)("th",K,"Edit")):(0,l.kq)("",!0),e.tableData.length?((0,l.wg)(),(0,l.iD)("th",O,"Delete")):(0,l.kq)("",!0)])]),e.tableData.length>0?((0,l.wg)(),(0,l.iD)("tbody",J,[((0,l.wg)(!0),(0,l.iD)(l.HY,null,(0,l.Ko)(e.tableData,(t=>((0,l.wg)(),(0,l.iD)("tr",{key:t},[((0,l.wg)(!0),(0,l.iD)(l.HY,null,(0,l.Ko)(e.fields,(e=>((0,l.wg)(),(0,l.iD)("td",{key:e.key},(0,m.zw)(e.formatter?e.formatter(t[e.key]):t[e.key]),1)))),128)),(0,l._)("td",null,[(0,l._)("button",{class:"btn btn-secondary",onClick:a=>e.$emit("edit",t)}," Edit ",8,Q)]),(0,l._)("td",null,[(0,l._)("button",{class:"btn btn-danger",onClick:a=>e.$emit("delete",t)}," Delete ",8,ee)])])))),128))])):((0,l.wg)(),(0,l.iD)("tbody",te,[(0,l._)("tr",null,[(0,l._)("td",{colspan:e.fields.length,class:"text-center"}," No results found ",8,ae)])]))])])])}var le=(0,l.aZ)({name:"TableComponent",props:{tableData:{type:Array,required:!0},fields:{type:Array,required:!0},sortBy:{type:String,default:""}},setup(e){const t=(0,_.iH)(""),a=(0,_.iH)(!0);(0,l.YP)((()=>e.tableData),(()=>{if(t.value){let o=t.value;a.value?e.tableData.sort(((e,t)=>e[o]>t[o]?1:-1)):e.tableData.sort(((e,t)=>e[o]<t[o]?1:-1))}else if(e.sortBy&&e.fields.find((t=>t.key===e.sortBy)))o(e.sortBy);else{let t=e.fields.find((e=>e.sortable));t&&o(t.key)}}));const o=o=>{t.value===o?(e.tableData.reverse(),a.value=!a.value):(e.tableData.sort(((e,t)=>e[o]>t[o]?1:-1)),t.value=o,a.value=!0)},n=e=>{const o=a.value?"ascending":"descending";return t.value===e?o:"none"},r=e=>{this.$emit("delete",e)};return{sortTable:o,getAriaSort:n,deleteItem:r}}});const ne=(0,r.Z)(le,[["render",oe],["__scopeId","data-v-133eb1e4"]]);var re=ne,se=(0,l.aZ)({name:"Domains",components:{TableComponent:re},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"context",sortable:!0,label:"Context"},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime}],o=async e=>{t("delete",e.name,"domain")},l=async e=>{t("edit",e,"domain")};return{fields:a,deleteRecord:o,editRecord:l}}});const de=(0,r.Z)(se,[["render",W]]);var ie=de;const ce=(0,l._)("h2",null,"A Records",-1);function ue(e,t,a,o,n,r){const s=(0,l.up)("TableComponent");return(0,l.wg)(),(0,l.iD)("div",null,[ce,(0,l.Wm)(s,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var me=(0,l.aZ)({name:"ARecords",components:{TableComponent:re},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"IPv4 Address(s)",formatter:e=>e.join(" | ")},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"A")},l=async e=>{t("edit",e,"A")};return{fields:a,deleteRecord:o,editRecord:l}}});const pe=(0,r.Z)(me,[["render",ue]]);var be=pe;const fe=(0,l._)("h2",null,"AAAA Records",-1);function ve(e,t,a,o,n,r){const s=(0,l.up)("TableComponent");return(0,l.wg)(),(0,l.iD)("div",null,[fe,(0,l.Wm)(s,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var ye=(0,l.aZ)({name:"AAAARecords",components:{TableComponent:re},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"IPv6 Address(s)",formatter:e=>e.join(" | ")},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"AAAA")},l=async e=>{t("edit",e,"AAAA")};return{fields:a,deleteRecord:o,editRecord:l}}});const he=(0,r.Z)(ye,[["render",ve]]);var De=he;const we=(0,l._)("h2",null,"CNAME Records",-1);function Te(e,t,a,o,n,r){const s=(0,l.up)("TableComponent");return(0,l.wg)(),(0,l.iD)("div",null,[we,(0,l.Wm)(s,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var ge=(0,l.aZ)({name:"CNAMERecords",components:{TableComponent:re},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"cname",sortable:!1,label:"CNAME"},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"CNAME")},l=async e=>{t("edit",e,"CNAME")};return{fields:a,deleteRecord:o,editRecord:l}}});const Ae=(0,r.Z)(ge,[["render",Te]]);var _e=Ae;const ke=(0,l._)("h2",null,"MX Records",-1);function Ce(e,t,a,o,n,r){const s=(0,l.up)("TableComponent");return(0,l.wg)(),(0,l.iD)("div",null,[ke,(0,l.Wm)(s,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var Re=(0,l.aZ)({name:"MXRecords",components:{TableComponent:re},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"Values",formatter:e=>{let t=e.map((e=>`${e.exchange} (Preference ${e.preference})`));return t.join(" | ")}},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"MX")},l=async e=>{t("edit",e,"MX")};return{fields:a,deleteRecord:o,editRecord:l}}});const Ee=(0,r.Z)(Re,[["render",Ce]]);var qe=Ee;const Me=(0,l._)("h2",null,"TXT Records",-1);function xe(e,t,a,o,n,r){const s=(0,l.up)("TableComponent");return(0,l.wg)(),(0,l.iD)("div",null,[Me,(0,l.Wm)(s,{class:"mb-5",onDelete:e.deleteRecord,onEdit:e.editRecord,fields:e.fields,tableData:e._data,sortBy:"created"},null,8,["onDelete","onEdit","fields","tableData"])])}var Ie=(0,l.aZ)({name:"TXTRecords",components:{TableComponent:re},props:{_data:{},formatDateTime:Function},emits:["delete","edit"],setup(e,{emit:t}){const a=[{key:"name",sortable:!0,label:"Subdomain Name"},{key:"values",sortable:!1,label:"Values",formatter:e=>e.join(" | ")},{key:"created",sortable:!0,label:"Created",formatter:e.formatDateTime},{key:"updated",sortable:!0,label:"Updated",formatter:e.formatDateTime},{key:"ttl",sortable:!0,label:"TTL"}],o=async e=>{t("delete",e.name,"TXT")},l=async e=>{t("edit",e,"TXT")};return{fields:a,deleteRecord:o,editRecord:l}}});const Ze=(0,r.Z)(Ie,[["render",xe]]);var $e=Ze;const Se={class:"container"},je={class:"mb-3"},Ue=(0,l._)("label",{for:"action"},"Action:",-1),Ve=(0,l.uE)('<option value="Claim Domain">Claim Domain</option><option value="A">Create A Record</option><option value="AAAA">Create AAAA Record</option><option value="CNAME">Create CNAME Record</option><option value="MX">Create MX Record</option><option value="TXT">Create TXT Record</option>',6),ze=[Ve],Xe={class:"mb-3"},Ne=(0,l._)("label",{for:"domain"},"Domain:",-1),He={key:0,class:"mb-3"},Le=(0,l._)("label",{for:"context"},"Context:",-1),Fe={key:1,class:"mb-3"},We=(0,l._)("label",{for:"values"},"Enter one or more IPv4 Addresses (one per line):",-1),Be=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),Pe={key:2,class:"mb-3"},Ye=(0,l._)("label",{for:"values"},"Enter one or more IPv6 Addresses (one per line):",-1),Ge=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),Ke={key:3,class:"mb-3"},Oe=(0,l._)("label",{for:"cname"},"Target:",-1),Je=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),Qe={key:4,class:"mb-3"},et=(0,l._)("label",{for:"values"},"Exchange server, preference (one per line):",-1),tt=["placeholder"],at=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),ot={key:5,class:"mb-3"},lt=(0,l._)("label",{for:"values"},"Values (one per line):",-1),nt=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),rt=["disabled"],st={key:0},dt=(0,l._)("span",{class:"spinner-border spinner-border-sm",role:"status","aria-hidden":"true"},null,-1),it=(0,l._)("span",{class:"sr-only"}," Loading...",-1),ct=[dt,it],ut={key:1};function mt(e,t,a,n,r,s){return(0,l.wg)(),(0,l.iD)("div",Se,[(0,l._)("form",{onSubmit:t[14]||(t[14]=(0,o.iM)(((...e)=>s.submitForm&&s.submitForm(...e)),["prevent"]))},[(0,l._)("div",je,[Ue,(0,l.wy)((0,l._)("select",{class:"form-select",id:"action","onUpdate:modelValue":t[0]||(t[0]=e=>r.selectedAction=e),onChange:t[1]||(t[1]=(...e)=>s.resetForm&&s.resetForm(...e))},ze,544),[[o.bM,r.selectedAction]])]),(0,l._)("div",Xe,[Ne,(0,l.wy)((0,l._)("input",{class:"form-control",type:"text",id:"domain","onUpdate:modelValue":t[2]||(t[2]=e=>r.domain=e),required:""},null,512),[[o.nr,r.domain]])]),"Claim Domain"===r.selectedAction?((0,l.wg)(),(0,l.iD)("div",He,[Le,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"Context such as 2023 Fall",type:"text",id:"context","onUpdate:modelValue":t[3]||(t[3]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]])])):(0,l.kq)("",!0),"A"===r.selectedAction?((0,l.wg)(),(0,l.iD)("div",Fe,[We,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1.1.1.1\n2.2.2.2","onUpdate:modelValue":t[4]||(t[4]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),Be,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[5]||(t[5]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,l.kq)("",!0),"AAAA"===r.selectedAction?((0,l.wg)(),(0,l.iD)("div",Pe,[Ye,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1111::1111\n2222::2222","onUpdate:modelValue":t[6]||(t[6]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),Ge,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[7]||(t[7]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,l.kq)("",!0),"CNAME"===r.selectedAction?((0,l.wg)(),(0,l.iD)("div",Ke,[Oe,(0,l.wy)((0,l._)("input",{class:"form-control mb-3",type:"text",id:"cname",placeholder:"example.com","onUpdate:modelValue":t[8]||(t[8]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),Je,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[9]||(t[9]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,l.kq)("",!0),"MX"===r.selectedAction?((0,l.wg)(),(0,l.iD)("div",Qe,[et,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"mailinator.com, 1\nmail.exchange.com, 3","onUpdate:modelValue":t[10]||(t[10]=e=>r.formInput=e),required:""},null,8,tt),[[o.nr,r.formInput]]),at,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[11]||(t[11]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,l.kq)("",!0),"TXT"===r.selectedAction?((0,l.wg)(),(0,l.iD)("div",ot,[lt,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"A TXT Record\nAnother value","onUpdate:modelValue":t[12]||(t[12]=e=>r.formInput=e),required:""},null,512),[[o.nr,r.formInput]]),nt,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[13]||(t[13]=e=>r.ttl=e),required:""},null,512),[[o.nr,r.ttl]])])):(0,l.kq)("",!0),(0,l._)("button",{disabled:r.loading,class:"form-control btn btn-success",type:"submit"},[r.loading?((0,l.wg)(),(0,l.iD)("div",st,ct)):((0,l.wg)(),(0,l.iD)("span",ut,"Submit"))],8,rt)],32)])}var pt=a(4161),bt={data(){return{selectedAction:"Claim Domain",domain:"",loading:!1,formInput:"",ttl:3600}},methods:{resetForm(){this.domain="",this.formInput="",this.ttl=3600},async submitForm(){try{let e,t=[];switch(this.loading=!0,this.selectedAction){case"Claim Domain":e=await pt.Z.post(`/api/zone/${this.domain}`,{context:this.formInput},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"A":t=this.formInput.split("\n"),t=t.map((e=>e.trim())),e=await pt.Z.post(`/api/zone/${this.domain}/A`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"AAAA":t=this.formInput.split("\n"),t=t.map((e=>e.trim())),e=await pt.Z.post(`/api/zone/${this.domain}/AAAA`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"CNAME":e=await pt.Z.post(`/api/zone/${this.domain}/CNAME`,{cname:this.formInput,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"MX":t=this.formInput.split("\n"),t=t.map((e=>e.trim().split(","))),t=t.map((([e,t])=>({preference:t.trim(),exchange:e.trim()}))),e=await pt.Z.post(`/api/zone/${this.domain}/MX`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"TXT":t=this.formInput.split("\n"),e=await pt.Z.post(`/api/zone/${this.domain}/TXT`,{values:t,ttl:this.ttl},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break}this.loading=!1,this.resetForm(),201===e.status&&(this.$emit("getZone"),this.$emit("showSuccess",this.selectedAction))}catch(e){this.loading=!1,this.$emit("showError",this.selectedAction,e)}}}};const ft=(0,r.Z)(bt,[["render",mt]]);var vt=ft;const yt={class:"container"},ht={class:"mb-3"},Dt=(0,l._)("label",{for:"domain"},"Domain:",-1),wt={key:0,class:"mb-3"},Tt=(0,l._)("label",{for:"context"},"Context:",-1),gt={key:1,class:"mb-3"},At=(0,l._)("label",{for:"values"},"Enter one or more IPv4 Addresses (one per line):",-1),_t=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),kt={key:2,class:"mb-3"},Ct=(0,l._)("label",{for:"values"},"Enter one or more IPv6 Addresses (one per line):",-1),Rt=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),Et={key:3,class:"mb-3"},qt=(0,l._)("label",{for:"cname"},"Target:",-1),Mt=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),xt={key:4,class:"mb-3"},It=(0,l._)("label",{for:"values"},"Exchange server, preference (one per line):",-1),Zt=["placeholder"],$t=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),St={key:5,class:"mb-3"},jt=(0,l._)("label",{for:"values"},"Values (one per line):",-1),Ut=(0,l._)("label",{for:"ttl"},"TTL (seconds):",-1),Vt=["disabled"],zt={key:0},Xt=(0,l._)("span",{class:"spinner-border spinner-border-sm",role:"status","aria-hidden":"true"},null,-1),Nt=(0,l._)("span",{class:"sr-only"}," Loading...",-1),Ht=[Xt,Nt],Lt={key:1};function Ft(e,t,a,n,r,s){return(0,l.wg)(),(0,l.iD)("div",yt,[(0,l._)("form",{onSubmit:t[12]||(t[12]=(0,o.iM)(((...e)=>n.submitForm&&n.submitForm(...e)),["prevent"]))},[(0,l._)("div",ht,[Dt,(0,l.wy)((0,l._)("input",{disabled:"",class:"form-control",type:"text",id:"domain","onUpdate:modelValue":t[0]||(t[0]=e=>n.domain=e),required:""},null,512),[[o.nr,n.domain]])]),"domain"===a.type?((0,l.wg)(),(0,l.iD)("div",wt,[Tt,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"Context such as 2023 Fall",type:"text",id:"context","onUpdate:modelValue":t[1]||(t[1]=e=>n.formInput=e),required:""},null,512),[[o.nr,n.formInput]])])):(0,l.kq)("",!0),"A"===a.type?((0,l.wg)(),(0,l.iD)("div",gt,[At,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1.1.1.1\n2.2.2.2","onUpdate:modelValue":t[2]||(t[2]=e=>n.formInput=e),required:""},null,512),[[o.nr,n.formInput]]),_t,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[3]||(t[3]=e=>n.ttl=e),required:""},null,512),[[o.nr,n.ttl]])])):(0,l.kq)("",!0),"AAAA"===a.type?((0,l.wg)(),(0,l.iD)("div",kt,[Ct,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"1111::1111\n2222::2222","onUpdate:modelValue":t[4]||(t[4]=e=>n.formInput=e),required:""},null,512),[[o.nr,n.formInput]]),Rt,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[5]||(t[5]=e=>n.ttl=e),required:""},null,512),[[o.nr,n.ttl]])])):(0,l.kq)("",!0),"CNAME"===a.type?((0,l.wg)(),(0,l.iD)("div",Et,[qt,(0,l.wy)((0,l._)("input",{class:"form-control mb-3",type:"text",id:"cname",placeholder:"example.com","onUpdate:modelValue":t[6]||(t[6]=e=>n.formInput=e),required:""},null,512),[[o.nr,n.formInput]]),Mt,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[7]||(t[7]=e=>n.ttl=e),required:""},null,512),[[o.nr,n.ttl]])])):(0,l.kq)("",!0),"MX"===a.type?((0,l.wg)(),(0,l.iD)("div",xt,[It,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"mailinator.com, 1\nmail.exchange.com, 3","onUpdate:modelValue":t[8]||(t[8]=e=>n.formInput=e),required:""},null,8,Zt),[[o.nr,n.formInput]]),$t,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[9]||(t[9]=e=>n.ttl=e),required:""},null,512),[[o.nr,n.ttl]])])):(0,l.kq)("",!0),"TXT"===a.type?((0,l.wg)(),(0,l.iD)("div",St,[jt,(0,l.wy)((0,l._)("textarea",{id:"values",class:"form-control mb-3",placeholder:"A TXT Record\nAnother value","onUpdate:modelValue":t[10]||(t[10]=e=>n.formInput=e),required:""},null,512),[[o.nr,n.formInput]]),Ut,(0,l.wy)((0,l._)("input",{class:"form-control",placeholder:"3600",type:"number","onUpdate:modelValue":t[11]||(t[11]=e=>n.ttl=e),required:""},null,512),[[o.nr,n.ttl]])])):(0,l.kq)("",!0),(0,l._)("button",{disabled:n.loading,class:"form-control btn btn-success",type:"submit"},[n.loading?((0,l.wg)(),(0,l.iD)("div",zt,Ht)):((0,l.wg)(),(0,l.iD)("span",Lt,"Submit"))],8,Vt)],32)])}var Wt={props:{record:{},type:""},emits:["getZone","showSuccess","showError"],setup(e,{emit:t}){const a=(0,_.iH)(""),o=(0,_.iH)(!1),n=(0,_.iH)(""),r=(0,_.iH)(3600);(0,l.m0)((()=>{"domain"===e.type?n.value=e.record.context:"CNAME"===e.type?n.value=e.record.cname:"MX"===e.type?n.value=e.record.values.map((e=>`${e.exchange}, ${e.preference}`)).join("\n"):n.value=e.record.values.join("\n"),a.value=e.record.name,r.value=e.record.ttl}));const s=()=>{a.value="",n.value="",r.value=3600},d=async()=>{try{let l,d=[];switch(o.value=!0,e.type){case"domain":l=await pt.Z.put(`/api/zone/${a.value}`,{context:n.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"A":case"AAAA":case"TXT":d=n.value.split("\n"),l=await pt.Z.put(`/api/zone/${a.value}/${e.type}`,{values:d,ttl:r.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"CNAME":l=await pt.Z.put(`/api/zone/${a.value}/${e.type}`,{cname:n.value,ttl:r.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break;case"MX":d=n.value.split("\n").map((e=>{const[t,a]=e.split(",");return{exchange:t,preference:parseInt(a)}})),l=await pt.Z.put(`/api/zone/${a.value}/${e.type}`,{values:d,ttl:r.value},{headers:{"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"}});break}o.value=!1,s(),console.log(l.status),200===l.status&&(t("getZone"),t("showSuccess",e.type,!0))}catch(l){o.value=!1,t("showError",e.type,l,!0)}};return{domain:a,loading:o,formInput:n,ttl:r,resetForm:s,submitForm:d}}};const Bt=(0,r.Z)(Wt,[["render",Ft]]);var Pt=Bt,Yt=(0,l.aZ)({components:{Modal:L,Domains:ie,ARecords:be,AAAARecords:De,CNAMERecords:_e,MXRecords:qe,TXTRecords:$e,Action:vt,EditRecord:Pt},name:"DNSRecords",setup(){const e=(0,_.iH)({domains:[],aRecords:[],aaaaRecords:[],cnameRecords:[],mxRecords:[],txtRecords:[]}),t=((0,_.iH)(""),(0,_.iH)(!1)),a=(0,_.iH)(""),o=(0,_.iH)(""),n=(0,_.iH)({}),r=(0,_.iH)(""),s=(0,_.iH)(""),d=(0,_.iH)(!1),i=(0,_.iH)(!1),c=(0,_.iH)(!1),u=(0,_.iH)(!1),m=(0,_.iH)(""),p=(0,_.iH)(""),b=(0,_.iH)(!1),f=(0,_.iH)(!1),v=async()=>{i.value=!0;try{const t="/api/zone",a={"Content-Type":"application/json",Accept:"application/json, */*;q=0.1"},o=await pt.Z.get(t,{headers:a});200===o.status&&(e.value=o.data)}catch(t){g("fetch",t),console.error("Error fetching data:",t)}i.value=!1},y=(e,l)=>{b.value=!1,f.value=!1,p.value="Delete Record",t.value=!0,a.value=e,o.value=l,s.value="domain"===l?"Deleting a domain will delete all records associated with it. Are you sure you want to continue?":`Are you sure you want to delete this ${l} record?`},h=(e,a)=>{b.value=!1,f.value=!0,p.value="Edit Record",t.value=!0,n.value=e,r.value=a},D=async e=>{if(e&&a.value&&o.value)try{let e="";e="domain"===o.value?`/api/zone/${a.value}`:`/api/zone/${a.value}/${o.value}`,d.value=!0;const t=await pt.Z.delete(e);200===t.status&&(T("delete"),await v())}catch(l){d.value=!1,g(`delete ${o.value}`,l)}d.value=!1,t.value=!1,a.value="",o.value="",s.value=""},w=e=>{const t={year:"numeric",month:"short",day:"numeric",hour:"numeric",minute:"numeric"},a=new Date(e);return a.toLocaleString(void 0,t)},T=(e,a=!1)=>{t.value=!1,u.value=!0,m.value="delete"===e?"Record deleted successfully.":`${e} record ${a?"updated":"created"} successfully.`,setTimeout((()=>{m.value="",u.value=!1}),3e3)},g=(e,a,o=!1)=>{t.value=!1,c.value=!0,a.response?.data?.message?m.value="Error: "+a.response.data.message:m.value=`There was an error ${o?"updating":"creating"} the ${e} record.`,setTimeout((()=>{m.value="",c.value=!1}),1e4)},A=()=>{p.value="Add Record",t.value=!0,f.value=!1,b.value=!0};return(0,l.bv)(v),{zoneData:e,showModal:t,deleteName:a,deleteType:o,deleteMessage:s,editRecord:n,editType:r,loading:d,zoneLoading:i,negativeAlert:c,positiveAlert:u,alertText:m,modalTitle:p,addingRecord:b,editingRecord:f,handleDelete:y,handleEdit:h,handleCloseModal:D,formatDateTime:w,showSuccess:T,showError:g,getZone:v,addRecord:A}}});const Gt=(0,r.Z)(Yt,[["render",A],["__scopeId","data-v-e20cc0e6"]]);var Kt=Gt,Ot={name:"ZoneView",components:{DNSRecords:Kt}};const Jt=(0,r.Z)(Ot,[["render",u]]);var Qt=Jt;const ea=[{path:"/",name:"home",component:Qt},{path:"/login",name:"login",component:()=>a.e(443).then(a.bind(a,6678))}],ta=(0,c.p7)({history:(0,c.PO)("/c/"),routes:ea});var aa=ta;a(5654);const oa=(0,o.ri)(i);oa.use(aa),pt.Z.interceptors.request.use((e=>(e.headers["Authorization"]=`Bearer ${sessionStorage.getItem("token")}`,e)),(e=>Promise.reject(e))),pt.Z.interceptors.response.use((e=>{const t=e.headers.get("Authentication-Info");if(t){let e="";for(let a of t.split(",")){a=a.trim();let t=a.search("=");if(t<0)continue;let o=t;while(o>0&&(" "==a[o-1]||"\t"==a[o-1]))--o;if(13==o&&"bearer-update"==a.substring(0,13).toLowerCase()){a.substring(t+1).trim(),e=a.substring(t+1).trim(),sessionStorage.setItem("token",e);break}}}return e})),oa.mount("#app")}}]);
//# sourceMappingURL=chunk-common.af586112.js.map