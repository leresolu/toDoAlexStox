<script setup>
import axios from 'axios';

const toDos = ref([]);

onMounted(() => {
    axios('https://localhost:7213/api/ToDos')
        .then(response => {toDos.value = response.data});

    setInterval(() => {
        axios('https://localhost:7213/api/ToDos')
            .then(response => {toDos.value = response.data});
    }, 10000);
})
</script>

<template>
    <div class="flex-box">
        <AddCard/>
        <ToDoCard v-for="toDo in toDos" :key="toDo.id" :toDo="toDo"></ToDoCard>
    </div>
</template>
<style>
.flex-box {
    display: flex;
    flex-direction: column;
    flex-wrap: wrap;
    gap: 10px;
    align-content: flex-start;
}
</style>