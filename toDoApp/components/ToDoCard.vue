<script setup>
import axios from 'axios'

const props = defineProps(['toDo'])
const { toDo } = props;
const priorityColors = ref(["secondary", "info", "primary", "success", "warning", "danger"])

const showForm = ref(false);

const submit = (updatedToDo) => {
    showForm.value = false;
    axios.put(`https://localhost:7213/api/ToDos/${updatedToDo.id}`, updatedToDo, {withCredentials: false})
        .then((response => console.log(response.data)));
}

const toggleForm = () => {
    showForm.value = !showForm.value
}
</script>

<template>
    <va-card
    class="toDoCard"
    stripe
    :stripe-color="priorityColors[toDo.priority]"
    >
    <va-badge v-if="toDo.dueDate"
        :text="'Due on ' + toDo.dueDate"
        color="danger"
    />
    <va-card-title>
        <div class="card-title-section">
            <div>{{toDo.title}}</div>
            <va-button @click="toggleForm" icon="edit" round size="small" preset="primary" class="showOnHover"></va-button>
        </div>
    </va-card-title>
    <va-card-content>{{ toDo.content }}</va-card-content>
    <va-badge
        :text="'Created on ' + toDo.createdDate"
        color="secondary"
    />
    </va-card>
    <to-do-form v-if="showForm" :is-shown="showForm" :to-do="toDo" @submit="submit" @cancel="toggleForm"></to-do-form>
</template>
<style>
.toDoCard {
    width: 240px;
    max-height: 480px;
    overflow-y: auto;
    overflow-x: hidden;
}

.toDoCard .showOnHover {
    opacity: 0;
    transition: opacity 0.3s;
}

.toDoCard:hover .showOnHover {
    opacity: 1;
}
.card-title-section {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}
</style>