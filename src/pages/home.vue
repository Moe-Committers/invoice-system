<template>
  <Layout>
    <div class="container mx-auto p-4">
      <h1 class="text-2xl font-bold mb-4">Nuxt Counter Example</h1>
  
      <CounterButtons
        :count="counter"
        @increment="incrementCounter"
        @decrement="decrementCounter"
      />
  
      <p class="mt-4">Current count: {{ counter }}</p>
    </div>
  </Layout>
</template>

<script setup>

const counter = useState("counter", () => 0);

const incrementCounter = () => {
  counter.value++;
};

const decrementCounter = () => {
  counter.value--;
};
</script>
