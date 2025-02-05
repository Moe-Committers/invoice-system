<template>
  <div class="flex items-center gap-4">
    <button
      @click="$emit('decrement')"
      class="px-4 py-2 bg-red-500 text-white rounded hover:bg-red-600"
    >
      Decrease
    </button>

    <span class="text-xl font-semibold">{{ count }}</span>

    <button
      @click="$emit('increment')"
      class="px-4 py-2 bg-green-500 text-white rounded hover:bg-green-600"
    >
      Increase
    </button>
  </div>
</template>

<script setup>
defineProps({
  count: {
    type: Number,
    required: true,
  },
});

defineEmits(["increment", "decrement"]);
</script>
