<script setup>
import axios from 'axios';

const showForm = ref(false);

const buttonClass = computed(() => {
    return showForm.value ? "transparent" : "solid"
});

const toggleForm = () => {
    showForm.value = !showForm.value;
}

const submit = (newToDo) => {
    showForm.value = false;
    axios.post('https://localhost:7213/api/ToDos', newToDo, {withCredentials: false})
        .then((response => console.log(response.data)));
}

</script>

<template>
    <va-card
        class="add-button"
        stripe
        stripe-color="primary">
        <add-button @click="toggleForm" :class="buttonClass"></add-button>
    </va-card>
    <ToDoForm :is-shown="showForm" @submit="submit" @cancel="toggleForm"/>
</template>

<style>
.add-button {
    display: flex;
    flex-direction: column;
    cursor: pointer;
    opacity: 0.5;
    transition: opacity 0.3s;
}

.add-button:hover {
    opacity: 1;
}

.add-button-icon {
    align-self: center;
}

.transparent {
    opacity: 0.2;
    transition: opacity 0.3s;
}

.solid {
    opacity: 1;
    transition: opacity 0.3s;
}
</style>