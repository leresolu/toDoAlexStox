<script setup>
import { v4 as uuidv4 } from 'uuid';
import axios from 'axios'

const emit = defineEmits(['cancel', 'submit'])

const props = defineProps(['isShown', 'toDo'])

const priorities = [
    {label: "None", value: 0},
    {label: "Highest", value: 1},
    {label: "High", value: 2},
    {label: "Normal", value: 3},
    {label: "Low", value: 4},
    {label: "Lowest", value: 5}
];

const formClass = computed(() => {
    return props.isShown ? "form-open" : "form-closed"
});

const model = ref(props.toDo || {
    ownerId: 'd8a5212f-1e0a-4094-9fa7-3a809496c68c',
    title: '',
    content: '',
    dueDate: null,
    priority: 0
});

const isUpdating = computed(() => {
    return !!model.value.id
})

const showDeleteModal = ref(false);

const formTitle = computed(() => {
    return model.value.id ? 'Update a task' : 'Add a task'
})

const closeForm = () => {
    model.value = {
        ownerId: 'd8a5212f-1e0a-4094-9fa7-3a809496c68c',
        title: '',
        content: '',
        dueDate: null,
        priority: 0
    }
    emit('cancel');
}

const submit = () => {
    model.value.id = model.value.id ?? uuidv4(); // assign new GUID if none
    emit('submit', model.value)
    model.value = {
        ownerId: 'd8a5212f-1e0a-4094-9fa7-3a809496c68c',
        title: '',
        content: '',
        dueDate: null,
        priority: 0
    }
}

const deleteToDo = () => {
    axios.delete(`https://localhost:7213/api/ToDos/${props.toDo.id}`, {withCredentials: false})
        .then((response => console.log(response.data)));
    emit('cancel');
}
</script>

<template>
    <div class="sliding-form"
        :class="formClass">
        <va-card
            stripe
            stripe-color="primary"
            class="form-card"
        >
            <va-card-title>
                <div class="card-title-section">
                    <div>{{formTitle}}</div>
                    <va-button v-if="isUpdating"
                        @click="showDeleteModal = true"
                        icon="delete" round size="small"
                        preset="primary" color="danger"
                        class="showOnHover">
                    </va-button>
                </div>
            </va-card-title>
            <va-card-content class="overflow">
                <va-form class="flow-down">
                    <va-input
                        v-model="model.title"
                        label="Title"
                        />
        
                    <va-textarea
                    v-model="model.content"
                    label="Content"
                    />
                
                    <va-date-input 
                    v-model="model.dueDate"
                    label="Due Date"
                    manual-input
                    clearable
                    />

                    <va-select
                    v-model="model.priority"
                    :options="priorities"
                    text-by="label"
                    value-by="value"
                    label="Priority"
                    />
                    <div style="display:flex; flex-direction:row; justify-content: space-between; width:100%;">
                        <cancel-button @click="closeForm"></cancel-button>
                        <confirm-button @click="submit"></confirm-button>
                    </div>
                </va-form>    
            </va-card-content>
        </va-card>
        <va-modal v-model="showDeleteModal" @ok="deleteToDo" @cancel="showDeleteModal = false">
            <h3 class="va-h3">Careful!</h3>
            <p>
            You are about to permanently delete this item. This action cannot be undone. Do you want to continue?
            </p>
        </va-modal>
    </div>
</template>
<style>
.flow-down {
    display: flex;
    flex-direction: column;
    gap: 10px;
    align-items: baseline;
}

.sliding-form {
    z-index: 10;
    position: fixed !important;
    top: 0;
    height: 100vh;
    transition: left 0.3s;
    overflow-y:auto;
    overflow-x: hidden;
}

.showOnHover {
    opacity: 0.3;
    transition: opacity 0.3s;
}

.showOnHover:hover {
    opacity: 1;
}

.form-card {
    padding-left: 1vw;
    padding-right: 2vw;
    display: flex;
    flex-direction: column;
    overflow-y: auto;
}

.form-open {
    left: 0;
}

.form-closed {
    left: -100vw;
}
.card-title-section {
    width: 100%;
    display: flex;
    flex-direction: row;
    justify-content: space-between;
}
</style>